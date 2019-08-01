using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;

namespace FileManager.Models.Database.DepartmentsDocuments
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<UserDepartmentRole> UserDepartmentRoles { get; set; }

        public List<DepartmentsDocument> DepartmentsDocuments { get; set; }
    }
}
