using DocumentAnalyzerAPI.D.Models;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.A.ViewModels
{
    public class FileViewModel
    {
        /// <summary>
        /// Atribute of FileViewModel
        /// </summary>
        public IEnumerable<File> Files { get; set; }
    }
}
