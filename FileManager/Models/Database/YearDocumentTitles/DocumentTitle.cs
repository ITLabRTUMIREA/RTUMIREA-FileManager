using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.ReportingYearDocumentTitles
{ 
    public class DocumentTitle
    {
        public DocumentTitle() { }
        public DocumentTitle(string name, Guid documentTitleId, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            DocumentTypeId = documentTitleId;
            Description = description;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        public List<ReportingYearDocumentTitle> ReportingYearDocumentTitles { get; set; }
    }
}
