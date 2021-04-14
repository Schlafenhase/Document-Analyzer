using DocumentAnalyzerAPI.D.Models;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.D.Interfaces
{
    public interface IFileRepository
    {
        /// <summary>
        /// Method that gets all files in the database
        /// </summary>
        /// <returns>
        /// List with all files
        /// </returns>
        IEnumerable<File> GetFiles();

        /// <summary>
        /// Method that adds a file to the database
        /// </summary>
        /// <param name="file">
        /// File to be added
        /// </param>
        void AddFile(File file);
    }
}
