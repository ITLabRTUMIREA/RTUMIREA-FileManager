using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.DepartmentsDocuments;

namespace FileManager.Models.Database.DocumentStatus
{
    public class DocumentStatusHistory
    {
        public Guid Id { get; set; }
        public string CommentEdits { get; set; }

        public Guid DocumentStatusId { get; set; }
        public DocumentStatus DocumentStatus { get; set; }

        public Guid DepartmentsDocumentId { get; set; }
        public DepartmentsDocument DepartmentsDocument { get; set; }


    }
}
