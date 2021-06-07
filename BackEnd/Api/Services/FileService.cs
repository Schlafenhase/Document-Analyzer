using DAApi.Interfaces;
using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Atribute with the database context
        /// </summary>
        public DocumentAnalyzerDBContext _context;

        /// <summary>
        /// Constructor of FileService
        /// </summary>
        /// <param name="context">
        /// Database context
        /// </param>
        public FileService(DocumentAnalyzerDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that gets all files in the database
        /// </summary>
        /// <returns>
        /// List with all the files
        /// </returns>
        public IEnumerable<File> GetFiles()
        {
            return _context.Files;
        }

        /// <summary>
        /// Method that adds an files to the database
        /// </summary>
        /// <param name="file">
        /// File to be added
        /// </param>
        public void AddFile(File file)
        {
            _context.Add(file);
            _context.SaveChanges();
            return;
        }
    }
}
