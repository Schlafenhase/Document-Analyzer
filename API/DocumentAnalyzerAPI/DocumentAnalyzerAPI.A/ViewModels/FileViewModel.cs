using DocumentAnalyzerAPI.D.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.ViewModels
{
    public class FileViewModel
    {
        public IEnumerable<File> Files { get; set; }
    }
}
