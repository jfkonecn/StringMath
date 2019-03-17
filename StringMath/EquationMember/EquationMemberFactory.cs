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

        internal FactoryResult CreateEquationMember(string equationString, IEquationMember previousMember)
        {

            List<Func<string, IEquationMember, FactoryResult>> parsers =
                new List<Func<string, IEquationMember, FactoryResult>>
                {
                    TryGetBinaryOperator,
                    TryGetUnaryOperator,
                    TryToExtractANumber,
                    TryToExtractVariable,
                    TryGetFunction,
                    (s, mem) => TryGetBracket(s),
                    (s, mem) => TryGetFunctionArgumentSeparator(s)
                };

            FactoryResult result = null;
            foreach (var fun in parsers)
            {
                result = fun(equationString, previousMember);
                if (result != null) break;
            }
            return result;
        }

        private FactoryResult TryToExtractANumber(string equationString, IEquationMember previousMember)
        {

            FactoryResult result = RegularExpressionParser(
                Number.RegularExpression, 
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
                Variable.RegularExpression,
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
                foreach (IOperatorMember obj in BinaryOperator.AllOperators)
                {

                    FactoryResult result = RegularExpressionParser(
                            obj.RegularExpression,
                            equationString,
                            (x) => obj);
                    if (result != null) return result;
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
                (!(previousMember is IOperatorMember) ||
                previousMember.Equals(Bracket.RightBracket));
        }


        private FactoryResult TryGetUnaryOperator(string equationString, IEquationMember previousMember)
        {
            if (UnaryOperatorHasValidPreviousOperator(previousMember))
            {
                foreach (IOperatorMember obj in UnaryOperator.AllOperators)
                {
                    FactoryResult result = RegularExpressionParser(
                            obj.RegularExpression,
                            equationString,
                            (x) => obj);
                    if (result != null) return result;
                }
            }
            return null;
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
        /// 
        /// </summary>
        /// <param name="equationString"></param>
        /// <param name="previousOperator"></param>
        /// <param name="opt">null if BinaryOperator cannot be made</param>
        /// <returns>True if object can be created</returns>
        private FactoryResult TryGetBracket(string equationString)
        {
            FactoryResult result = RegularExpressionParser(
                Bracket.LeftBracket.RegularExpression,
                equationString,
                (x) => Bracket.LeftBracket);
            if (result != null)
            {
                return result;
            }

            return RegularExpressionParser(
                Bracket.RightBracket.RegularExpression,
                equationString,
                (x) => Bracket.RightBracket);
        }


        private FactoryResult TryGetFunction(string equationString, IEquationMember previousMember)
        {
            FactoryResult result = RegularExpressionParser(
                    Function.RegularExpression,
                    equationString,
                    (x) => new Function(x));

            return result;
        }

        private FactoryResult TryGetFunctionArgumentSeparator(string equationString)
        {
            return RegularExpressionParser(
                FunctionArgumentSeparator.RegularExpression,
                equationString,
                (x) => new FunctionArgumentSeparator());
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
