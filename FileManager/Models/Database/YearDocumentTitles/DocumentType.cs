using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.ReportingYearDocumentTitles
{
    public class DocumentType
    {
        public DocumentType() { }
        public DocumentType(string type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }
        public Guid Id { get; set; }
        public string Type { get; set; }

        public List<DocumentTitle> DocumentTitles { get; set; }
    }
}
