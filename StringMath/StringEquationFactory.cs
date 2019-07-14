using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath
{
    public class StringEquationFactory : IStringEquationFactory
    {
        public IStringEquation CreateStringEquation(string stringEquation)
        {
            return new StringEquation(stringEquation);
        }
    }
}
