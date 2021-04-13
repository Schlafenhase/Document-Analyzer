using DocumentAnalyzerAPI.A.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DocumentAnalyzerAPI.T
{
    [TestClass]
    public class NLPServiceTest
    {
        /// <summary>
        /// Atribute that stores the NLP Service
        /// </summary>
        NLPService nlpService = new NLPService();

        /// <summary>
        /// Method that uses the nlp library to search for all the entities a string
        /// </summary>
        [TestMethod]
        public void NLPAnalysisTest()
        {
            string text = "My name is Kevin and I like to eat pizza. My name is Debbie and I like to sleep. My name is John and I like to drink.";

            var doc = nlpService.NLPAnalysis(text).Result;
            List<string> names = new List<string>();

            foreach (var entity in doc.SelectMany(span => span.GetEntities()))
            {
                names.Add(entity.Value);
            }

            Assert.AreEqual(names[0], "Kevin");
            Assert.AreEqual(names[1], "Debbie");
            Assert.AreEqual(names[2], "John");
        }

        /// <summary>
        /// Method that deletes all the repeated spaces in a string
        /// </summary>
        [TestMethod]
        public void DeleteRepeatedSpacesTest()
        {
            string text = "This is a test                   this should work";

            string result = nlpService.DeleteRepeatedSpaces(text);

            Assert.AreEqual(result, "This is a test this should work");
        }
    }
}