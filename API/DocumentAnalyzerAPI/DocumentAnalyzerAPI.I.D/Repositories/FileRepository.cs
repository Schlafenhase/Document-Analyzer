using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.D.Models;
using DocumentAnalyzerAPI.I.D.Context;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.I.D.Repositories
{
    public class FileRepository : IFileRepository
    {
        /// <summary>
        /// Atribute that stores the microsoft sql database context
        /// </summary>
        public EntitiesDbContext _context;

        /// <summary>
        /// Constructor of FileRespository
        /// </summary>
        /// <param name="context">
        /// Entities database context
        /// </param>
        public FileRepository(EntitiesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that gets all files in the database
        /// </summary>
        /// <returns>
        /// List with all files
        /// </returns>
        public IEnumerable<File> GetFiles()
        {
            return _context.Files;
        }

        /// <summary>
        /// Method that adds a file to the database
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
