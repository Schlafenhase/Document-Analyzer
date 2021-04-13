using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Models;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Method that gets all the files in the database
        /// </summary>
        /// <returns>
        /// List with all the files
        /// </returns>
        FileViewModel GetFiles();

        /// <summary>
        /// Method that adds a file to the database
        /// </summary>
        /// <param name="file">
        /// File to be added
        /// </param>
        void AddFile(File file);
    }
}
