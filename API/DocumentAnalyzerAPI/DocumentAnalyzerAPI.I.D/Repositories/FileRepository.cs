using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.D.Models;
using DocumentAnalyzerAPI.I.D.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.I.D.Repositories
{
    public class FileRepository : IFileRepository
    {
        public EntitiesDbContext _context;
        public FileRepository(EntitiesDbContext context)
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
