using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class DepartmentsDocumentsVersion
    {
        public DepartmentsDocumentsVersion(Guid departmentsDocumentId, string fileName, string path,int lastVersion = 0)
        {
            Id = Guid.NewGuid();
            FileName = fileName;
            Path = path;
            Version = ++lastVersion;
            DepartmentDocumentId = departmentsDocumentId;
        }
        public Guid Id { get; set; }

        public string FileName { get; set; }
        public string Path { get; set; }

        public int Version { get; set; }

        public Guid DepartmentDocumentId { get; set; }
        public DepartmentsDocument DepartmentsDocument { get; set; }
    }
}
