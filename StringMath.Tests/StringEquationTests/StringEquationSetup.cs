using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMath.Tests.StringEquationTests
{
    [SetUpFixture]
    public class StringEquationSetup
    {        
        private static IStringEquationFactory StringEquationFactory { get; set; }

        public static IStringEquation BuildStringEquation(string strEqu)
        {
            return StringEquationFactory.CreateStringEquation(strEqu);
        }

        [OneTimeSetUp]
        public void Setup()
        {
            StringEquationFactory = new StringEquationFactory();
        }
    }
}
