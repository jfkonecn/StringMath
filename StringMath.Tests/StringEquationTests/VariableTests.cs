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
        [TestCase("($0 - 273.15) * 9 / 5 + 32", 212, 373.15)]
        [TestCase("($0 - 32) * 5 / 9 + 273.15", 373.15, 212)]
        public void Variable(string equStr, double exp, params double[] nums)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(equStr);

            double result = eq.Evaluate(nums);

            Assert.That(eq.EquationArguments.Count(), Is.EqualTo(nums.Length));
            for (int i = 0; i < nums.Length; i++)
            {
                Assert.That(eq.EquationArguments.ElementAt(i), Is.EqualTo(i.ToString()));
            }
            Assert.That(result, Is.EqualTo(exp).Within(0.1).Percent);
        }

        [Test]
        [TestCase("$My.Good - $Inputs.Are.Fun", 40, 50, 10)]
        public void VarNameTests(string equStr, double exp, params double[] nums)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(equStr);

            double result = eq.Evaluate(nums);

            Assert.That(eq.EquationArguments.Count(), Is.EqualTo(nums.Count()));
            Assert.That(eq.EquationArguments.ElementAt(0), Is.EqualTo("My.Good"));
            Assert.That(eq.EquationArguments.ElementAt(1), Is.EqualTo("Inputs.Are.Fun"));
            Assert.That(result, Is.EqualTo(exp).Within(0.1).Percent);
        }

        [TestCase("$VolumetricFlowRate / ($InletPipeArea * \r\n                            Sqrt((2 * $PressureDrop) / ($Density * \r\n                                ($InletPipeArea ^ 2 / $OrificeArea ^ 2 - 1))))", 0, 1, 1, 1, 1, 1)]
        public void ComplieTests(string equStr, double exp, params double[] nums)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(equStr);

            double result = eq.Evaluate(nums);

            Assert.That(eq.EquationArguments.Count(), Is.EqualTo(nums.Count()));
            Assert.That(result, Is.EqualTo(exp).Within(0.1).Percent);
        }
    }
}
