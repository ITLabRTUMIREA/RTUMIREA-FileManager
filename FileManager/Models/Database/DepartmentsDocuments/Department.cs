using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartments;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class Department
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<UserDepartment> UserDepartments { get; set; }

        public List<DepartmentsDocument> DepartmentsDocuments { get; set; }
    }
}
