using DocumentAnalyzerAPI.A.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace DocumentAnalyzerAPI.T
{
    [TestClass]
    public class AzureBlobAuthServiceTest
    {
        /// <summary>
        /// Atribute that stores the Azure Blob Auth Service
        /// </summary>
        AzureBlobAuthService azureBlobAuthService = new AzureBlobAuthService();

        /// <summary>
        /// Method that makes two SAS tokens, and it checks if they are different. The purpose is to make sure that the tokens are always different for security reasons 
        /// </summary>
        [TestMethod]
        public void DifferentSASTokensTest()
        {
            string key = "qNsyw45eu8Sd5TsnmaX4pzKRjD+Httj+JsNLWLYu5GQZeeXkdhU+49yBqacKgxe7PIEFEpDnP5wJNyVYFu/A7Q==";
            string account = "TestAcc";

            string SASToken1 = azureBlobAuthService.GenerateSAS(key, account);

            Thread.Sleep(1000);

            string SASToken2 = azureBlobAuthService.GenerateSAS(key, account);

            Assert.AreNotEqual(SASToken1, SASToken2);
        }
    }
}