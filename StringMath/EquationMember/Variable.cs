using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class Variable : IEquationMember
    {
        internal Variable(string name)
        {
            Name = name;
        }
        /// <summary>
        /// The lookup index of the variable
        /// </summary>
        internal string Name { get; }

        string IEquationMember.RegularExpression => Variable.RegularExpression;

        public static string RegularExpression => @"^\s*\$[\w_]+[\w\d]*";
    }
}
