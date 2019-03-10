﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class UnaryOperator : IOperator
    {
        private UnaryOperator(string regularExpression, Func<double, double> evaluator)
        {
            RegularExpression = regularExpression;
            Evaluator = evaluator;
        }

        /// <summary>
        /// i.e. "-" (without quotes) if this is a negative operator
        /// </summary>
        internal readonly string RegularExpression;

        private readonly Func<double, double> Evaluator;

        internal double Evaluate(double num)
        {
            return Evaluator(num);
        }

        public double Evaluate(ref Stack<double> vs)
        {
            if (TotalParameters > vs.Count)
                throw new ArgumentException();
            double num = vs.Pop();
            return Evaluator(num);
        }

        public static readonly List<UnaryOperator> AllOperators =
        new List<UnaryOperator>()
        {
            new UnaryOperator(@"^\s*\+\b",
                (double num)=>{ return num; }),
            new UnaryOperator(@"^\s*[-−]\b",
                (double num)=>{ return -num; })
        };

        public ushort Precedence { get { return 4; } }

        public OperatorAssociativity Associativity { get { return OperatorAssociativity.RightAssociative; } }

        public ushort TotalParameters { get { return 1; } }




    }
}
