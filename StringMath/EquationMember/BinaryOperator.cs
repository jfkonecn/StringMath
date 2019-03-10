using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class BinaryOperator : IOperatorMember
    {
        private BinaryOperator(string regularExpression, ushort precedence,
    OperatorAssociativity associativity, Func<double, double, double> evaluator)
        {
            RegularExpression = regularExpression;
            Associativity = associativity;
            Evaluator = evaluator;
            Precedence = precedence;
        }

        /// <summary>
        /// i.e. "+" (without quotes) if this is an addition operator
        /// </summary>
        public string RegularExpression { get; }


        public ushort Precedence { get; }

        public OperatorAssociativity Associativity { get; }

        public ushort TotalParameters { get { return 2; } }

        private readonly Func<double, double, double> Evaluator;


        public double Evaluate(Stack<double> vs)
        {
            if (TotalParameters > vs.Count)
                throw new ArgumentException();
            double rightNum = vs.Pop(), leftNum = vs.Pop();
            return Evaluator(leftNum, rightNum);
        }


        // https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        // https://en.wikipedia.org/wiki/Order_of_operations
        public static readonly List<BinaryOperator> AllOperators =
            new List<BinaryOperator>()
            {
                new BinaryOperator(@"^\s*\^", 4, OperatorAssociativity.RightAssociative,
                    (double left, double right)=>{ return Math.Pow(left, right); }),
                new BinaryOperator(@"^\s*\*", 3, OperatorAssociativity.LeftAssociative,
                    (double left, double right)=>{ return left * right; }),
                new BinaryOperator(@"^\s*/", 3, OperatorAssociativity.LeftAssociative,
                    (double left, double right)=>{ return left / right; }),
                new BinaryOperator(@"^\s*\+", 2, OperatorAssociativity.LeftAssociative,
                    (double left, double right)=>{ return left + right; }),
                new BinaryOperator(@"^\s*[-−]", 2, OperatorAssociativity.LeftAssociative,
                    (double left, double right)=>{ return left - right; })
            };
    }
}
