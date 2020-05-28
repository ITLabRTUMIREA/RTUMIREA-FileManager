using FileManager.Models.Database.UserDepartmentRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class DepartmentsDocumentsVersion
    {
        public DepartmentsDocumentsVersion() { }
        public DepartmentsDocumentsVersion(Guid departmentsDocumentId, string fileName, string path, DateTime uploadedDateTime, Guid userId)
        {
            FileName = fileName;
            Path = path;
            UploadedDateTime = uploadedDateTime;
            DepartmentDocumentId = departmentsDocumentId;
            UserId = userId;
        }
        public Guid Id { get; set; }

        public string FileName { get; set; }
        public string Path { get; set; }

        public DateTime UploadedDateTime { get; set; }

        public Guid DepartmentDocumentId { get; set; }
        public DepartmentsDocument DepartmentsDocument { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
