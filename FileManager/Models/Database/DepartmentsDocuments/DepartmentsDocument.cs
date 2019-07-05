﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.DocumentStatus;
using FileManager.Models.Database.YearDocumentTitles;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class DepartmentsDocument
    {
        public Guid ID { get; set; }

        public Guid YearDocumentTitleID { get; set; }
        public YearDocumentTitle YearDocumentTitle { get; set; }

        public Guid DepartmentID { get; set; }
        public Department Department { get; set; }

        public List<DepartmentsDocumentsVersion> DepartmentsDocumentsVersions { get; set; }

        public List<DocumentStatusHistory> DocumentStatusHistories { get; set; }
    }
}
