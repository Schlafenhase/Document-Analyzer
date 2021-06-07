using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Method that gets all files in the database
        /// </summary>
        /// <returns>
        /// List with all the files
        /// </returns>
        IEnumerable<File> GetFiles();

        /// <summary>
        /// Method that adds an files to the database
        /// </summary>
        /// <param name="file">
        /// File to be added
        /// </param>
        void AddFile(File file);
    }
}
