using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.Tests.StringEquationTests
{
    [TestFixture]
    public class OrderOfOperationsTests
    {
        [Test]
        [TestCase(" 3 + 4 * 2 / ( 1 − 5 ) ^ 2 ^ 3 ", 3)]
        [TestCase("20 * 2 - (1/2) * 9.8 * 2^2", 20.4)]
        [TestCase("4^3^2", 262144)]
        [TestCase("7 + (6 * 5^2 + 3)", 160)]
        public void OrderOfOperations(string strEq, double exp)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(strEq);
            Assert.AreEqual(3, eq.Evaluate(), 1e-3);
        }
    }
}
