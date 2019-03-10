using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class Number : IEquationMember
    {
        internal Number(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
