using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class DocumentType
    {
        public Guid ID { get; set; }
        public string Type { get; set; }
        public List<DocumentName> DocumentNames { get; set; }
    }
}
