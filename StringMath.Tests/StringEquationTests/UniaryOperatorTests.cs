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
        [TestCase(1)]
        public void UniaryPlusShouldWork(double x)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation($"+{x}");
            Assert.AreEqual(eq.Evaluate(x), x);
        }

        [Test]
        [TestCase(1)]
        public void UniaryMinusShouldWork(double x)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation($"-{x}");
            Assert.AreEqual(eq.Evaluate(x), -x);
        }
    }
}
