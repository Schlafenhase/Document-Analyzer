using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface IFileService
    {
        FileViewModel GetFiles();

        void AddFile(File file);
    }
}
