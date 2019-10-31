using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class DepartmentsDocumentsVersion
    {
        public DepartmentsDocumentsVersion() { }
        public DepartmentsDocumentsVersion(Guid departmentsDocumentId, string fileName, string path, DateTime uploadedDateTime)
        {
            FileName = fileName;
            Path = path;
            UploadedDateTime = uploadedDateTime;
            DepartmentDocumentId = departmentsDocumentId;
        }
        public Guid Id { get; set; }

        public string FileName { get; set; }
        public string Path { get; set; }

        public DateTime UploadedDateTime { get; set; }

        public Guid DepartmentDocumentId { get; set; }
        public DepartmentsDocument DepartmentsDocument { get; set; }
    }
}
