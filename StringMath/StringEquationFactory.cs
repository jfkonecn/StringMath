using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath
{
    public class StringEquationFactory : IStringEquationFactory
    {
        public IStringEquation CreateStringEquation(string stringEquation, params string[] parameterNames)
        {
            return new StringEquation(stringEquation, parameterNames);
        }
    }
}
