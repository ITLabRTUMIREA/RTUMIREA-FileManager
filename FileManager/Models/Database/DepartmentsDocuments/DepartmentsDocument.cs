using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.DocumentStatus;
using FileManager.Models.Database.ReportingYearDocumentTitles;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class DepartmentsDocument
    {
        public Guid Id { get; set; }

        public Guid ReportingYearDocumentTitleId { get; set; }
        public ReportingYearDocumentTitle ReportingYearDocumentTitle { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<DepartmentsDocumentsVersion> DepartmentsDocumentsVersions { get; set; }

        public List<DocumentStatusHistory> DocumentStatusHistories { get; set; }
    }
}
