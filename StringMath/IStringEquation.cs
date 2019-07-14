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

        /// <summary>
        /// The arguments required to evaluate the equation in the order they need to be 
        /// passed into evaluate, this will contain nothing if no arguments are found
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<string> EquationArguments { get; }
    }
}
