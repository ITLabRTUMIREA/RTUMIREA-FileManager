﻿using System;
using System.Collections.Generic;
using FileManager.Models.Database.DocumentStatuses;
using FileManager.Models.Database.ReportingYearDocumentTitles;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class DepartmentsDocument
    {
        public DepartmentsDocument(Guid departmentId, Guid reportingYearDocumentTitleId)
        {
            Id = Guid.NewGuid();
            DepartmentId = departmentId;
            ReportingYearDocumentTitleId = reportingYearDocumentTitleId;
            DocumentStatusHistories = new List<DocumentStatusHistory>();
        }
        public Guid Id { get; set; }

        public Guid ReportingYearDocumentTitleId { get; set; }
        public ReportingYearDocumentTitle ReportingYearDocumentTitle { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid LastSetDocumentStatusId { get; set; }
        public List<DepartmentsDocumentsVersion> DepartmentsDocumentsVersions { get; set; }

        public List<DocumentStatusHistory> DocumentStatusHistories { get; set; }
    }
}
