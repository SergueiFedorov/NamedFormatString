using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF;
using System.Collections.Generic;

namespace Unit_Tests
{
    [TestClass]
    public class ParamsTests
    {
        [TestMethod]
        public void ValidParams()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");
            parameters.Add("SecondSlot", "ReplacementTwo");

            string resultString = NamedFormatString.Format("{FirstSlot} {SecondSlot}", parameters);

            Assert.IsNotNull(resultString);
            Assert.AreEqual(resultString, "ReplacementOne ReplacementTwo");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidParams_Enclosed_Fractured_Brackets()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");
            parameters.Add("SecondSlot", "ReplacementTwo");

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{FirstSlot {SecondSlot}", parameters);
        }

        [TestMethod]
        public void ValidParams_Enclosed_Fractured_Double_Brackets()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");
            parameters.Add("SecondSlot", "ReplacementTwo");

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{{FirstSlot {SecondSlot}", parameters);

            Assert.IsNotNull(resultString);
            Assert.AreEqual(resultString, "{FirstSlot ReplacementTwo");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidParams_Enclosed_Missing_Close_Bracket()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{FirstSlot", parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidParams_Enclosed_Missing_Open_Bracket()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("FirstSlot}", parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EmptyParams()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            //This is valid behavior because the brackets are enclosed
            string resultString = NamedFormatString.Format("{FirstSlot}", parameters);
        }

        [TestMethod]
        public void NestedBrackets()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");

            //This is actually a valid behavior from String.Format
            string resultString = NamedFormatString.Format("{{FirstSlot}}", parameters);

            Assert.IsNotNull(resultString);
            Assert.AreEqual(resultString, "{FirstSlot}");
        }
    }
}
