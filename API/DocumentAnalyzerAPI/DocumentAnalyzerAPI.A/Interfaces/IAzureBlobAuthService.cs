using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface IAzureBlobAuthService
    {
        string GenerateSAS(string key, string accountName);
    }
}
