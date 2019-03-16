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

        public double Evalute(params double[] args)
        {
            throw new NotImplementedException();
        }



        private Queue<IEquationMember> ReversePolishNotationQueue { get; set; }

        /// <summary>
        /// Using Shunting yard algorithm
        /// </summary>
        /// <param name="equationString"></param>
        /// <param name="varFinder"></param>
        /// <returns></returns>
        private Queue<IEquationMember> CreateReversePolishNotationQueue(string equationString, params string[] parameterNames)
        {
            /*
             https://en.wikipedia.org/wiki/Shunting-yard_algorithm
             http://wcipeg.com/wiki/Shunting_yard_algorithm#Unary_operators
             http://tutplusplus.blogspot.com/2011/12/c-tutorial-arithmetic-expression.html
             http://tutplusplus.blogspot.com/2010/12/c-tutorial-equation-calculator.html
             */


            Stack<IOperatorMember> operatorStack = new Stack<IOperatorMember>();
            Queue<IEquationMember> outputQueue = new Queue<IEquationMember>();
            IEquationMember previousToken = null;
            EquationMemberFactory factory = EquationMemberFactory.Factory;
            while (equationString.Length > 0)
            {
                int startingLength = equationString.Length;

                FactoryResult result = factory.CreateEquationMember(equationString, previousToken);
                previousToken = result.Member;
                equationString = result.RemainingString;
                // if we didn't do anything in a loop, then there are unsupported strings
                if (startingLength == equationString.Length)
                    throw new ArgumentException();
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() as Bracket != null)
                    throw new ArgumentException();
                outputQueue.Enqueue(operatorStack.Pop());
            }
            return outputQueue;
        }

    }
}

