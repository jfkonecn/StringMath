using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class FactoryResult
    {
        internal IEquationMember Member { get; set; } = null;
        internal string RemainingString { get; set; } = null;
    }
}
