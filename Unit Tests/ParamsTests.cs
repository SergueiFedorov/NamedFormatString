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

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FormatStringHasMoreParameters()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");

            //This is actually a valid behavior from String.Format
            string resultString = NamedFormatString.Format("{FirstSlot} {SecondSlot}", parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFormat()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");

            NamedFormatString.Format(null, parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void NullArgumentList()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");

            NamedFormatString.Format("{FirstSlot} {SecondSlot}", null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AnynymousBrackets()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FirstSlot", "ReplacementOne");

            string resultString = NamedFormatString.Format("{{}}} {}", parameters);
        }

        [TestMethod]
        public void EscapedBrackets_1()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            string resultString = NamedFormatString.Format("{{}}", parameters);

            Assert.AreEqual(resultString, "{}");
        }

        [TestMethod]
        public void EscapedBrackets_2()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            string resultString = NamedFormatString.Format("{{}}{{", parameters);

            Assert.AreEqual(resultString, "{}{");
        }

        [TestMethod]
        public void EscapedBrackets_3()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            string resultString = NamedFormatString.Format("{{{{}}}}", parameters);

            Assert.AreEqual(resultString, "{{}}");
        }

    }
}
