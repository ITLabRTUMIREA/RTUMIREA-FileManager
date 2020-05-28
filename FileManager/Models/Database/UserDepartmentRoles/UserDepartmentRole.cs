using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.DepartmentsDocuments;
using Microsoft.AspNetCore.Identity;

namespace FileManager.Models.Database.UserDepartmentRoles
{
    public class UserDepartmentRole : IdentityUserRole<Guid>
    {
        public UserDepartmentRole() : base() { }

        public User User { get; set; }

        public Role Role { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
