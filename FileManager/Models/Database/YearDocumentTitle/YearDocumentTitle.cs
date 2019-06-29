using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class YearDocumentTitle
    {
        public Guid ID { get; set; }

        public Guid YearID { get; set; }
        public Year Year { get; set; }

        public Guid DocumentTitleID { get; set; }
        public DocumentTitle DocumentTitle { get; set; }

        public List<DepartamentsDocument> DepartamentsDocuments { get; set; }

    }
}
