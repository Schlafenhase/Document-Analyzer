using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IAnalysisService
    {
        /// <summary>
        /// Method that extracts the text from a file
        /// </summary>
        /// <param name="fileName">
        /// String with the fileName
        /// </param>
        /// <param name="fileContainer">
        /// String with the fileContainer
        /// </param>
        /// <param name="connectionString">
        /// String with the connections parameters
        /// </param>
        /// <returns>
        /// String with the text found
        /// </returns>
        string GetText(string fileName, string fileContainer, string connectionString);
    }
}
