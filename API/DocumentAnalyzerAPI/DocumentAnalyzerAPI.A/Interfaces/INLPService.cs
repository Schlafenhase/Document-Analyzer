﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface INLPService
    {
        void SearchEmployees(string fileName, string connectionString, string containerName, string apiKey);
    }
}
