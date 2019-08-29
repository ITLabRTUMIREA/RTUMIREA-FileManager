using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.YearDocumentTitles
{
    public class DocumentType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public List<DocumentTitle> DocumentTitles { get; set; }
    }
}
