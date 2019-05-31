using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class DepartamentsDocument
    {
        public Guid ID { get; set; }

        public Guid YearDocumentTitleID { get; set; }
        public YearDocumentTitle YearDocumentTitle { get; set; }

        public Guid DepartamentID { get; set; }
        public Departament Departament { get; set; }

        public List<DepartamentsDocumentsVersion> DepartamentsDocumentsVersions { get; set; }

        public List<DocumentStatusHistory> DocumentStatusHistories { get; set; }
    }
}
