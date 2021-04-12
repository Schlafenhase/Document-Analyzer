using DocumentAnalyzerAPI.D.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface INLPService
    {
        void SearchEmployees(File file, string connectionString, string containerName);
    }
}
