using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.Tests.StringEquationTests
{
    [TestFixture]
    public class VariableTests
    {
        [Test]
        [TestCase("$0 + $1", 60, 10, 50)]
        public void Variable(string equStr, double exp, params double[] nums)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(equStr);
            Assert.That(eq.Evaluate(nums), Is.EqualTo(exp).Within(0.1).Percent);
        }

        [Test]
        [TestCase("$My.Good - $Inputs.Are.Fun", 40, 10, 50)]
        public void VarNameTests(string equStr, double exp, params double[] nums)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(equStr,  "Inputs.Are.Fun", "My.Good");
            Assert.That(eq.Evaluate(nums), Is.EqualTo(exp).Within(0.1).Percent);
        }
    }
}
