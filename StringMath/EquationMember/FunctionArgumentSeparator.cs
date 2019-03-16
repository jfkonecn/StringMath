using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    public class FunctionArgumentSeparator : IEquationMember
    {
        string IEquationMember.RegularExpression => Function.RegularExpression;

        public static string RegularExpression => @"^\s*,";
    }
}
