using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IAnalysisService
    {
        string GetText(string fileName, string fileContainer, string connectionString);
    }
}
