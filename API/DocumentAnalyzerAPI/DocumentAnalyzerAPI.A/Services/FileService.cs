using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Services
{
    public class FileService : IFileService
    {
        public IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public FileViewModel GetFiles()
        {
            return new FileViewModel()
            {
                Files = _fileRepository.GetFiles()
            };
        }
    }
}
