using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.EquationMember
{
    internal class Bracket : IPrecedenceMember
    {
        private Bracket(string regularExpression) { RegularExpression = regularExpression; }
        public static readonly Bracket LeftBracket = new Bracket(@"^\s*\(");
        public static readonly Bracket RightBracket = new Bracket(@"^\s*\)");
        public string RegularExpression { get; }

        public ushort Precedence { get { return 5; } }
    }
}
