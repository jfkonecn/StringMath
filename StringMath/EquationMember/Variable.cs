using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class Variable : IEquationMember
    {
        internal Variable(int index)
        {
            Index = index;
        }
        /// <summary>
        /// The lookup index of the variable
        /// </summary>
        internal int Index { get; }

        string IEquationMember.RegularExpression => Variable.RegularExpression;
        public static string RegularExpression => $"{ReplaceRegularExpression}{@"[\w_]+[\.\w\d]*"}";
        public static string ReplaceRegularExpression => @"^\s*\$";
        //public static string RegularExpression => @"(?<=^\s*\$)[\w_]+[\w\d]*";
        //public static string RegularExpression => @"^\s*\$[\w_]+[\w\d]*";
    }
}
