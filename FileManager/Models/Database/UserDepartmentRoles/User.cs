using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Identity;

namespace FileManager.Models.Database.UserDepartmentRoles
{
    public class User : IdentityUser<Guid>
    {
        public string FistName { get; set; }
        public string LastName { get; set; }

        public List<UserDepartmentRole> UserRoleDepartments { get; set; }
    }
}
