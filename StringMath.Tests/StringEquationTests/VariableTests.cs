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

            double result = eq.Evaluate(nums);

            Assert.That(eq.EquationArguments.Count(), Is.EqualTo(2));
            Assert.That(eq.EquationArguments.ElementAt(0), Is.EqualTo("0"));
            Assert.That(eq.EquationArguments.ElementAt(1), Is.EqualTo("1"));
            Assert.That(result, Is.EqualTo(exp).Within(0.1).Percent);
        }

        [Test]
        [TestCase("$My.Good - $Inputs.Are.Fun", 40, 50, 10)]
        public void VarNameTests(string equStr, double exp, params double[] nums)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(equStr);

            double result = eq.Evaluate(nums);

            Assert.That(eq.EquationArguments.Count(), Is.EqualTo(2));
            Assert.That(eq.EquationArguments.ElementAt(0), Is.EqualTo("My.Good"));
            Assert.That(eq.EquationArguments.ElementAt(1), Is.EqualTo("Inputs.Are.Fun"));
            Assert.That(result, Is.EqualTo(exp).Within(0.1).Percent);
        }
    }
}
