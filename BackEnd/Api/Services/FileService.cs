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
        public DocumentAnalyzerDBContext _context;

        public FileService(DocumentAnalyzerDBContext context)
        {
            _context = context;
        }

        public IEnumerable<File> GetFiles()
        {
            return _context.Files;
        }

        public void AddFile(File file)
        {
            _context.Add(file);
            _context.SaveChanges();
            return;
        }
    }
}
