using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    public class EquationMemberFactory
    {
        public static EquationMemberFactory Factory { get; } = new EquationMemberFactory();

        private EquationMemberFactory()
        {

        }

        public FactoryResult CreateEquationMember(string stringEquation, IEquationMember previousMember)
        {
            if (Number.TryGetNumber(ref stringEquation, previousMember, varFinder, out Number num))
            {
                outputQueue.Enqueue(num);
                previousMember = num;
            }

            if (Function.TryGetFunction(ref stringEquation, previousMember, out Function temp))
            {
                curFun = temp;
                previousMember = temp;
                operatorStack.Push(curFun);
            }


            if (HelperFunctions.TryGetOperator(ref stringEquation, previousMember, out IOperator opt))
            {
                while (
                    operatorStack.Count > 0 &&
                    (operatorStack.Peek() is Function ||
                    operatorStack.Peek().Precedence > opt.Precedence ||
                    (operatorStack.Peek().Precedence == opt.Precedence && opt.Associativity == OperatorAssociativity.LeftAssociative))
                    && !operatorStack.Peek().Equals(Bracket.LeftBracket))
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                previousMember = opt;
                operatorStack.Push(opt);
            }

            if (Bracket.TryGetBracket(ref stringEquation, out Bracket bracket))
            {
                if (bracket.Equals(Bracket.LeftBracket))
                {
                    operatorStack.Push(bracket);
                }
                else if (bracket.Equals(Bracket.RightBracket))
                {
                    if (operatorStack.Count == 0)
                        throw new SyntaxException();
                    while (!operatorStack.Peek().Equals(Bracket.LeftBracket))
                    {
                        // Unbalanced  parentheses
                        if (operatorStack.Count < 1)
                            throw new SyntaxException();
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                }
                else
                {
                    throw new NotImplementedException();
                }
                previousMember = bracket;
            }

            if (Function.IsFunctionArgumentSeparator(ref stringEquation))
            {
                if (curFun == null)
                    throw new SyntaxException();
                curFun.TotalParameters++;
                previousMember = null;
            }

            HelperFunctions.RegularExpressionParser(@"^\s+$", ref stringEquation);
        }

        private FactoryResult TryToExtractANumber(string equationString, IEquationMember previousMember)
        {
            if (previousMember == null || !(previousMember is IOperator))
            {
                return null;
            }

            FactoryResult result = RegularExpressionParser(
                @"^\s*\d+(\.\d+)?([eE][-+]?\d+)?", 
                equationString, 
                (x) => new Number(double.Parse(x)));
            if (result != null)
            {
                return result;
            }

            return RegularExpressionParser(
                @"^\s*PI",
                equationString,
                (x) => new Number(Math.PI));
        }


        private FactoryResult TryToExtractVariable(string equationString, IEquationMember previousMember)
        {
            return RegularExpressionParser(
                @"^\s*\$[\w_]+[\w\d]*",
                equationString,
                (x) => new Variable(x));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="charArr">Changes only if a this method returns true</param>
        /// <param name="previousMember"></param>
        /// <param name="opt">null if BinaryOperator cannot be made</param>
        /// <returns>True if object can be created</returns>
        private FactoryResult TryGetBinaryOperator(string equationString, IEquationMember previousMember)
        {
            if (BinaryOperatorHasValidPreviousOperator(previousMember))
            {
                foreach (IOperator obj in BinaryOperator.AllOperators)
                {

                    FactoryResult result = RegularExpressionParser(
                            obj.RegularExpression,
                            equationString,
                            (x) => obj);
                }
            }
            return null;
        }


        /// <summary>
        /// true if the previous is valid for a binary operator
        /// </summary>
        /// <param name="previousToken"></param>
        /// <returns></returns>
        private bool BinaryOperatorHasValidPreviousOperator(IEquationMember previousMember)
        {
            return previousMember != null &&
                (!(previousMember is IOperator) ||
                previousMember.Equals(Bracket.RightBracket));
        }


        private bool TryGetUnaryOperator(ref string equationString,
    IEquationMember previousMember, out IOperator opt)
        {
            if (ValidPreviousOperator(previousToken))
            {
                foreach (UnaryOperator obj in AllOperators)
                {
                    if (HelperFunctions.RegularExpressionParser(obj.RegularExpression, ref equationString))
                    {
                        opt = obj;
                        return true;
                    }
                }
            }
            opt = null;
            return false;
        }

        /// <summary>
        /// true if the previous is valid for a unary operator
        /// </summary>
        /// <param name="previousMember"></param>
        /// <returns></returns>
        private bool UnaryOperatorHasValidPreviousOperator(IEquationMember previousMember)
        {
            return !BinaryOperatorHasValidPreviousOperator(previousMember);
        }

        /// <summary>
        /// If there is a match with the regExpression then a FactoryResult is returned
        /// </summary>
        /// <param name="regExpression">The string which represents the member the ie "+" or "sqrt"</param>
        /// <param name="equationString"></param>
        /// <param name="memberFactory">Given the matching string returns an IEquationMember</param>
        /// <returns>FactoryResult or null if no match found</returns>
        private FactoryResult RegularExpressionParser(string regExpression, string equationString, 
            Func<string, IEquationMember> memberFactory)
        {
            Regex reg = new Regex(regExpression);
            
            if (!reg.IsMatch(equationString))
            {
                return null;
            }  

            string matchString = reg.Match(equationString).Value;
            return new FactoryResult()
            {
                Member = memberFactory(matchString),
                RemainingString = reg.Replace(equationString, string.Empty)
            };
        }
    }
}
