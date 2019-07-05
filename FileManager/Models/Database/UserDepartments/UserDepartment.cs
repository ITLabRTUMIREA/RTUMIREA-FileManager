using System;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.UserRole;

namespace FileManager.Models.Database.UserDepartments
{
    public class UserDepartment
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
