using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using FileManager.Models.Database.DepartmentsDocuments;

namespace FileManager.Models.Database.ReportingYearDocumentTitles
{
    public class ReportingYearDocumentTitle
    {
        public Guid Id { get; set; }

        public Guid ReportingYearId { get; set; }
        public ReportingYear ReportingYear { get; set; }

        public Guid DocumentTitleId { get; set; }
        public DocumentTitle DocumentTitle { get; set; }

        public List<DepartmentsDocument> DepartmentsDocuments { get; set; }

    }
}
