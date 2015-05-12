using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF;

namespace Unit_Tests
{
    [TestClass]
    public class ParamsTests
    {
        [TestMethod]
        public void ValidParams()
        {
            string[] parameters = new[]
            {
                "ReplacementOne",
                "ReplacementTwo"
            };

            string resultString = NamedFormatString.Format("{FirstSlot} {SecondSlot}", parameters);

            Assert.IsNotNull(resultString);
            Assert.AreEqual(resultString, "ReplacementOne ReplacementTwo");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidParams_Enclosed_Fractured_Brackets()
        {
            string[] parameters = new[]
            {
                "ReplacementOne",
                "ReplacementTwo"
            };

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{FirstSlot {SecondSlot}", parameters);
        }

        [TestMethod]
        public void ValidParams_Enclosed_Fractured_Double_Brackets()
        {
            string[] parameters = new[]
            {
                "ReplacementOne",
                "ReplacementTwo"
            };

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{{FirstSlot {SecondSlot}", parameters);

            Assert.IsNotNull(resultString);
            Assert.AreEqual(resultString, "{FirstSlot ReplacementOne");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidParams_Enclosed_Missing_Close_Bracket()
        {

            string[] parameters = new[]
            {
                "ReplacementOne"
            };

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{FirstSlot", parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidParams_Enclosed_Missing_Open_Bracket()
        {
            string[] parameters = new[]
            {
                "ReplacementOne"
            };

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("FirstSlot}", parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EmptyParams()
        {
            string[] parameters = new string[]
            {

            };

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{FirstSlot}", parameters);
        }

        [TestMethod]
        public void NestedBrackets()
        {
            string[] parameters = new string[]
            {
                "ReplacementOne"
            };

            //This is actually a valid behavior from String.Format
            string resultString = NamedFormatString.Format("{{FirstSlot}}", parameters);

            Assert.IsNotNull(resultString);
            Assert.AreEqual(resultString, "{FirstSlot}");
        }
    }
}
