using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF;

namespace Unit_Tests
{
    [TestClass]
    public class DynamicObjTests
    {
        [TestMethod]
        public void SingleParameter()
        {
            string resultString = NamedFormatString.Format("{Variable}", new { Variable = "Replacement" });

            Assert.AreEqual(resultString, "Replacement");
        }

        [TestMethod]
        public void ClassPropertyDecimalToString()
        {
            string resultString = NamedFormatString.Format("{Variable} {SomeDecimal}", new { Variable = "Replacement", SomeDecimal = new Decimal(10) });

            Assert.AreEqual(resultString, "Replacement 10");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ClassPropertyNestedDynamic()
        {
            string resultString = NamedFormatString.Format("{Variable} {DynamicType}", new { Variable = "Replacement", DynamicType = new { MyVar = "SomeVar" } });
        }
    }
}
