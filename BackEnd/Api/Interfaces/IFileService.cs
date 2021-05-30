using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IFileService
    {
        IEnumerable<File> GetFiles();

        void AddFile(File file);
    }
}
