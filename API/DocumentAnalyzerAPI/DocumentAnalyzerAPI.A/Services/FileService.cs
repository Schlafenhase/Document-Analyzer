using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.D.Models;

namespace DocumentAnalyzerAPI.A.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Atribute that stores the file repository
        /// </summary>
        public IFileRepository _fileRepository;

        /// <summary>
        /// Constructor of FileService
        /// </summary>
        /// <param name="fileRepository">
        /// File repository
        /// </param>
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        /// <summary>
        /// Method that gets all the files in the database
        /// </summary>
        /// <returns>
        /// List with all the files
        /// </returns>
        public FileViewModel GetFiles()
        {
            return new FileViewModel()
            {
                Files = _fileRepository.GetFiles()
            };
        }

        /// <summary>
        /// Method that adds a file to the database
        /// </summary>
        /// <param name="file">
        /// File to be added
        /// </param>
        public void AddFile(File file)
        {
            _fileRepository.AddFile(file);
            return;
        }
    }
}
