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
        public List<DepartamentsDocumentsVersion> DepartamentsDocumentsVersions { get; set; }
        public List<DocumentStatusHistory> DocumentStatusHistoriesID { get; set; }
    }
}
