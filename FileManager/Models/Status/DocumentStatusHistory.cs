using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class DocumentStatusHistory
    {
        public Guid DocumentStatusID { get; set; }
        public Guid DepartamentsDocumentID { get; set; }
        public string CommentEdits { get; set; }

    }
}
