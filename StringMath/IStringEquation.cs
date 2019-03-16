using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath
{
    public interface IStringEquation
    {
        /// <summary>
        /// Evaluate the equation
        /// </summary>
        /// <param name="args">arguments required to evaluate the equation</param>
        /// <returns></returns>
        double Evaluate(params double[] args);
    }
}
