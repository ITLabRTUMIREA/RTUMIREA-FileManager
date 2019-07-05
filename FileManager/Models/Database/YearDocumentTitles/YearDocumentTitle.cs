using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using FileManager.Models.Database.DepartmentsDocuments;

namespace FileManager.Models.Database.YearDocumentTitles
{
    public class YearDocumentTitle
    {
        public Guid ID { get; set; }

        public Guid YearID { get; set; }
        public Year Year { get; set; }

        public Guid DocumentTitleID { get; set; }
        public DocumentTitle DocumentTitle { get; set; }

        public List<DepartmentsDocument> DepartmentsDocuments { get; set; }

    }
}
