using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class Function : IOperatorMember
    {
        internal Function(string methodName)
        {
            MethodInfo = typeof(Math).GetMethod(methodName, new Type[] { typeof(double) });
            if (MethodInfo == null)
                MethodInfo = typeof(Math).GetMethod(methodName);

            if (MethodInfo == null)
                throw new ArgumentException();
        }

        public ushort TotalParameters { get; internal set; } = 1;        

        private readonly MethodInfo MethodInfo;

        public ushort Precedence { get { return ushort.MaxValue; } }

        public OperatorAssociativity Associativity { get { return OperatorAssociativity.LeftAssociative; } }

        string IEquationMember.RegularExpression => Function.RegularExpression;

        public static string RegularExpression => @"^\s*[\w_]+[\w\d]*(?=\()";

        public double Evaluate(Stack<double> vs)
        {
            if (TotalParameters > vs.Count)
                throw new ArgumentException();

            Stack<object> nums = new Stack<object>();
            for (int i = 0; i < TotalParameters; i++)
            {
                nums.Push(vs.Pop());
            }

            try
            {
                return (double)MethodInfo.Invoke(null, nums.ToArray());
            }
            catch
            {
                throw new ArgumentException();
            }
        }
    }
}
