using DocumentAnalyzerAPI.D.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface INLPService
    {
        Dictionary<string, int> SearchEmployees(File file, string connectionString, string containerName);
    }
}
