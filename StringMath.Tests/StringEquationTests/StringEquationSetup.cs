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

        public static IStringEquation BuildStringEquation(string strEqu, params string[] parameterNames)
        {
            return StringEquationFactory.CreateStringEquation(strEqu, parameterNames);
        }

        [OneTimeSetUp]
        public void Setup()
        {
            StringEquationFactory = new StringEquationFactory();
        }
    }
}
