﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.D.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Progress { get; set; }
        public string URL { get; set; }
    }
}