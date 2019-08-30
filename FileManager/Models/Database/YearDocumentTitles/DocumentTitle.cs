﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.ReportingYearDocumentTitles
{ 
    public class DocumentTitle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        public List<ReportingYearDocumentTitle> ReportingYearDocumentTitles { get; set; }
    }
}
