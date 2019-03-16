using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.Tests.StringEquationTests
{
    [TestFixture]
    public class MathFunctionTests
    {
        [Test]
        [TestCase(100)]
        public void SqrtShouldWork(double x)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation($"Sqrt({x})");
            Assert.AreEqual(Math.Sqrt(x), eq.Evaluate(), 1e-3);
        }

        [Test]
        [TestCase(100, 10)]
        public void PowShouldWork(double x, double y)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation($"Pow({x},{y})");
            Assert.AreEqual(Math.Pow(x, y), eq.Evaluate(), 1e-3);
        }

        [Test]
        [TestCase(100)]
        public void PiShouldWork(double x)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation($"PI / 4 * {x} * {x}");
            Assert.AreEqual(Math.PI / 4 * x * x, eq.Evaluate(), 1e-3);
        }

        [Test]
        [TestCase(2)]
        public void FunctionWithinFunctionShouldWork(double x)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation($"Pow(Pow({x},{x}),{x})");
            Assert.AreEqual(Math.Pow(Math.Pow(x, x), x), eq.Evaluate(), 1e-3);
        }
    }
}
