using StringMath.EquationMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath
{
    internal class StringEquation : IStringEquation
    {
        public StringEquation(string stringEquation)
        {
            if (string.IsNullOrWhiteSpace(stringEquation))
                throw new ArgumentException(nameof(stringEquation));
            ReversePolishNotationQueue = CreateReversePolishNotationQueue(stringEquation);
        }

        public double Evaluate(params double[] args)
        {

            Stack<double> numStack = new Stack<double>();
            Queue<IEquationMember> newQueue = new Queue<IEquationMember>();
            while (ReversePolishNotationQueue.Count > 0)
            {
                IEquationMember curMember = ReversePolishNotationQueue.Dequeue();
                newQueue.Enqueue(curMember);
                if (curMember is IOperatorMember opt)
                {
                    numStack.Push(opt.Evaluate(numStack));
                }
                else if (curMember is Number num)
                {
                    numStack.Push(num.Value);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            if (numStack.Count != 1)
                throw new ArgumentException();
            ReversePolishNotationQueue = newQueue;
            return numStack.Peek();
        }



        private Queue<IEquationMember> ReversePolishNotationQueue { get; set; }




        /*
        https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        http://wcipeg.com/wiki/Shunting_yard_algorithm#Unary_operators
        http://tutplusplus.blogspot.com/2011/12/c-tutorial-arithmetic-expression.html
        http://tutplusplus.blogspot.com/2010/12/c-tutorial-equation-calculator.html
        */
        /// <summary>
        /// Using Shunting yard algorithm 
        /// </summary>
        /// <param name="equationString"></param>
        /// <param name="varFinder"></param>
        /// <returns></returns>
        private Queue<IEquationMember> CreateReversePolishNotationQueue(string equationString, params string[] parameterNames)
        {
            if (string.IsNullOrEmpty(equationString)) throw new ArgumentNullException(nameof(equationString));
            Stack<IPrecedenceMember> precedenceStack = new Stack<IPrecedenceMember>();
            Stack<Function> functionStack = new Stack<Function>();
            Queue<IEquationMember> outputQueue = new Queue<IEquationMember>();
            IEquationMember previousToken = null;
            EquationMemberFactory factory = EquationMemberFactory.Factory;
            int totalNumbers = 0;
            int totalBinaryOpts = 0;            
            while (equationString.Length > 0 && !string.IsNullOrWhiteSpace(equationString))
            {
                int startingLength = equationString.Length;

                FactoryResult result = factory.CreateEquationMember(equationString, previousToken);
                previousToken = result?.Member;
                equationString = result?.RemainingString;
                if (previousToken is Number) totalNumbers++;
                if (previousToken is BinaryOperator) totalBinaryOpts++;
                previousToken = HandleToken(precedenceStack, functionStack, outputQueue, previousToken);

                // if we didn't do anything in a loop, then there are unsupported strings
                if (equationString == null || startingLength == equationString.Length)
                    throw new ArgumentException();
            }

            if(totalNumbers != totalBinaryOpts + 1 && totalBinaryOpts > 0)
            {
                throw new ArgumentException("Binary Operators must have at 2 numbers to interact with");
            }

            while (precedenceStack.Count > 0)
            {
                if (precedenceStack.Peek() is Bracket)
                    throw new ArgumentException();
                outputQueue.Enqueue(precedenceStack.Pop());
            }
            return outputQueue;
        }

        private IEquationMember HandleToken(
            Stack<IPrecedenceMember> precedenceStack, Stack<Function> functionStack, 
            Queue<IEquationMember> outputQueue, IEquationMember previousToken)
        {
            if (previousToken is Number num)
            {
                outputQueue.Enqueue(num);
            }
            else if (previousToken is Function fun)
            {
                precedenceStack.Push(fun);
                functionStack.Push(fun);
            }
            else if (previousToken is IOperatorMember opt)
            {
                while (TopOfStackHasHigherPriorityOperator(precedenceStack, opt))
                {
                    outputQueue.Enqueue(precedenceStack.Pop());
                }
                precedenceStack.Push(opt);
            }
            else if (previousToken is Bracket bracket)
            {
                BracketHelper(precedenceStack, outputQueue, functionStack, bracket);
            }
            else if (previousToken is FunctionArgumentSeparator funSep)
            {
                if (functionStack.Count() < 1)
                    throw new ArgumentException();
                functionStack.Peek().TotalParameters++;
                previousToken = null;
            }
            return previousToken;
        }

        private void BracketHelper(
            Stack<IPrecedenceMember> precedenceStack, 
            Queue<IEquationMember> outputQueue, 
            Stack<Function> functionStack, Bracket bracket)
        {
            if (bracket.Equals(Bracket.LeftBracket))
            {
                precedenceStack.Push(bracket);
            }
            else if (precedenceStack.Count == 0)
            {
                throw new ArgumentException();
            }                
            else if (bracket.Equals(Bracket.RightBracket))
            {                
                while (!precedenceStack.Peek().Equals(Bracket.LeftBracket))
                {
                    if (precedenceStack.Count < 1)
                        throw new ArgumentException("Unbalanced parentheses");
                    outputQueue.Enqueue(precedenceStack.Pop());
                }
                precedenceStack.Pop();
            }
            else
            {
                throw new NotImplementedException();
            }

            if(precedenceStack.Peek() is Function)
            {
                functionStack.Pop();
            }
        }

        private bool TopOfStackHasHigherPriorityOperator(Stack<IPrecedenceMember> precedenceStack, IOperatorMember opt)
        {
            return precedenceStack.Count > 0 &&
                (precedenceStack.Peek() is Function ||
                precedenceStack.Peek().Precedence > opt.Precedence ||
                (precedenceStack.Peek().Precedence == opt.Precedence && opt.Associativity == OperatorAssociativity.LeftAssociative))
                && !precedenceStack.Peek().Equals(Bracket.LeftBracket);
        }

    }
}

