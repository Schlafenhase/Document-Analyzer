using System;
using System.Collections.Generic;

#nullable disable

namespace DAApi.Models
{
    public partial class File
    {
        /// <summary>
        /// Atributes of File
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Container { get; set; }
    }
}
