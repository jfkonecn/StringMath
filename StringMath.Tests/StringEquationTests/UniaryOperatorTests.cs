using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.Tests.StringEquationTests
{
    [TestFixture]
    public class UniaryOperatorTests
    {
        [Test]
        [TestCase("+ 1", 1)]
        [TestCase("+(1 + 1)", 2)]
        public void UniaryPlusShouldWork(string stringEquation, double expected)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(stringEquation);
            Assert.AreEqual(eq.Evaluate(), expected);
        }

        [Test]
        [TestCase("- 1", -1)]
        [TestCase("-(1 + 1)", -2)]
        public void UniaryMinusShouldWork(string stringEquation, double expected)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(stringEquation);
            Assert.AreEqual(eq.Evaluate(), expected);
        }
    }
}
