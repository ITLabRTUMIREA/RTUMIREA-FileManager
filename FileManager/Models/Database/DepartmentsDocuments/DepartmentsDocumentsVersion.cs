using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class DepartmentsDocumentsVersion
    {
        public Guid ID { get; set; }
        public Int16 Version { get; set; }

        public Guid DepartmentDocumentID { get; set; }
        public DepartmentsDocument DepartmentsDocument { get; set; }
    }
}
