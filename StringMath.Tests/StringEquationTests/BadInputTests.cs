using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.Tests.StringEquationTests
{
    [TestFixture]
    public class BadInputTests
    {
        [Test]
        [TestCase("badInput")]
        [TestCase("1++")]
        [TestCase("++1")]
        [TestCase("--1")]
        [TestCase("-+1")]
        [TestCase("+-1")]
        [TestCase("1 -")]
        [TestCase("1 *")]
        [TestCase("1 /")]
        public void EquationStringShouldThrowArgumentException(string strEq, string exMsg = null)
        {
            Assert.Throws<ArgumentException>(() => StringEquationSetup.BuildStringEquation(strEq), exMsg);
        }
    }
}
