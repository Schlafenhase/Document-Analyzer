using DocumentAnalyzerAPI.D.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.D.Interfaces
{
    public interface IFileRepository
    {
        IEnumerable<File> GetFiles();
    }
}
