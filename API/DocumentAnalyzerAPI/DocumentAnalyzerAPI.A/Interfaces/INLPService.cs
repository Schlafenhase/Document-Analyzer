using Catalyst;
using DocumentAnalyzerAPI.D.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface INLPService
    {
        /// <summary>
        /// Method that searches all the employees in a file
        /// </summary>
        /// <param name="file">
        /// File to be searched and added to the sql database
        /// </param>
        /// <param name="connectionString">
        /// Azure blob storage connection string
        /// </param>
        /// <param name="containerName">
        /// Azure blob storage container name
        /// </param>
        /// <returns>
        /// Dictionary with the results found
        /// </returns>
        Dictionary<string, int> SearchEmployees(File file, string connectionString, string containerName);

        /// <summary>
        /// Method that makes the nlp analysis on a string
        /// </summary>
        /// <param name="text">
        /// Text of the file
        /// </param>
        /// <returns>
        /// Document with the found entities
        /// </returns>
        Task<IDocument> NLPAnalysis(string text);

        /// <summary>
        /// Method that deletes all the repeated spaces in a string
        /// </summary>
        /// <param name="text">
        /// String that will be modified
        /// </param>
        /// <returns>
        /// String without repeated spaces
        /// </returns>
        string DeleteRepeatedSpaces(string text);
    }
}
