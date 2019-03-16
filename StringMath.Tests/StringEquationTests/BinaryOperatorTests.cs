using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.Tests.StringEquationTests
{
    public enum BinaryOperators
    {
        add,
        subtract,
        multiply,
        divide,
        pow,
        equals,
        notEqual,
        greaterThan,
        lessThan,
        greaterThanEqualTo,
        lessThanEqualTo
    }

    [TestFixture(BinaryOperators.add)]
    [TestFixture(BinaryOperators.subtract)]
    [TestFixture(BinaryOperators.multiply)]
    [TestFixture(BinaryOperators.divide)]
    [TestFixture(BinaryOperators.pow)]
    [TestFixture(BinaryOperators.equals)]
    [TestFixture(BinaryOperators.notEqual)]
    [TestFixture(BinaryOperators.greaterThan)]
    [TestFixture(BinaryOperators.lessThan)]
    [TestFixture(BinaryOperators.greaterThanEqualTo)]
    [TestFixture(BinaryOperators.lessThanEqualTo)]
    public class BinaryOperatorTests
    {
        public BinaryOperatorTests(BinaryOperators opt)
        {
            // for conditionals
            func = (x, y) => x.CompareTo(y);
            switch (opt)
            {
                case BinaryOperators.add:
                    func = (x, y) => x + y;
                    optStr = "+";
                    break;
                case BinaryOperators.subtract:
                    func = (x, y) => x - y;
                    optStr = "-";
                    break;
                case BinaryOperators.multiply:
                    func = (x, y) => x * y;
                    optStr = "*";
                    break;
                case BinaryOperators.divide:
                    func = (x, y) => x / y;
                    optStr = "/";
                    break;
                case BinaryOperators.pow:
                    func = (x, y) => Math.Pow(x, y);
                    optStr = "^";
                    break;
                case BinaryOperators.equals:
                    func = (x, y) => Convert.ToDouble(x == y);
                    optStr = "==";
                    break;
                case BinaryOperators.notEqual:
                    func = (x, y) => Convert.ToDouble(x != y);
                    optStr = "!=";
                    break;
                case BinaryOperators.greaterThan:
                    func = (x, y) => Convert.ToDouble(x > y);
                    optStr = ">";
                    break;
                case BinaryOperators.lessThan:
                    func = (x, y) => Convert.ToDouble(x < y);
                    optStr = "<";
                    break;
                case BinaryOperators.greaterThanEqualTo:
                    func = (x, y) => Convert.ToDouble(x >= y);
                    optStr = ">=";
                    break;
                case BinaryOperators.lessThanEqualTo:
                    func = (x, y) => Convert.ToDouble(x <= y);
                    optStr = "<=";
                    break;
                default:
                    throw new ArithmeticException(opt.ToString());
            }
        }

        private readonly Func<double, double, double> func;
        private readonly string optStr;

        [Test]
        [TestCase("{0}{1}{2}", 1, 1)]
        [TestCase("{0} {1}{2}", 9, 5)]
        [TestCase("{0}{1} {2}", 1, 1)]
        [TestCase("{0} {1}  {2}", 1, 1)]
        [TestCase("{0} {1}  {2}", 1, 1.5)]
        public void BinaryShouldWorkWithWhiteSpace(string strEq, double x, double y)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation(string.Format(strEq, x, optStr, y));
            Assert.AreEqual(func(x, y), eq.Evaluate(x, y), 0.001);
        }

        [Test]
        [TestCase(5.51, 68.1)]
        public void BinaryShouldWorkUnlessDivdeByZero(double x, double y)
        {
            IStringEquation eq = StringEquationSetup.BuildStringEquation($"{x}{optStr}{y}");
            if (y == 0 && optStr == "/")
            {
                Assert.Throws<DivideByZeroException>(() => eq.Evaluate(x, y));
            }
            else
            {
                Assert.AreEqual(eq.Evaluate(x, y), func(x, y), 0.001);
            }
        }

        [Test]
        public void RandomBinaryTests()
        {
            Random rnd = new Random(0);
            double x, y, low = -100, high = 100;
            for (int i = 0; i < 100; i++)
            {
                x = rnd.NextDouble() * (high - low) + low;
                y = rnd.NextDouble() * (high - low) + low;
                BinaryShouldWorkUnlessDivdeByZero(x, y);
            }
        }

    }
}
