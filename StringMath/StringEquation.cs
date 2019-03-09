using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath
{
    internal class StringEquation : IStringEquation
    {
        public StringEquation(string stringEquation)
        {
            if (string.IsNullOrWhiteSpace(stringEquation))
                throw new ArgumentException(nameof(stringEquation));

        }

        public double Evalute(params double[] args)
        {
            throw new NotImplementedException();
        }
    }
}
