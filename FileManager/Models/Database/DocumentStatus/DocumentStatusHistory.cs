using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class DocumentStatusHistory
    {
        public Guid ID { get; set; }
        public string CommentEdits { get; set; }

        public Guid DocumentStatusID { get; set; }
        public DocumentStatus DocumentStatus { get; set; }

        public Guid DepartamentsDocumentID { get; set; }
        public DepartamentsDocument DepartamentsDocument { get; set; }


    }
}
