using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.DocumentStatus;
using FileManager.Models.Database.UserSystemRoles;
using Microsoft.AspNetCore.Identity;

namespace FileManager.Models.Database.UserDepartmentRoles
{
    public class Role:IdentityRole<Guid>
    {
        public Role(string name) : base(name)
        {
        }

        public List<RoleStatus> RoleStatuses { get; set; }

        public List<UserDepartmentRole> UserRoleDepartments { get; set; }

        public List<UserSystemRole> UserRoles { get; set; }
    }
}
