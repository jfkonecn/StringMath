using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal interface IPrecedenceMember : IEquationMember
    {
        ushort Precedence { get; }
        string RegularExpression { get; }
    }
}
