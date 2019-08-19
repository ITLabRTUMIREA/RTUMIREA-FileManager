using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.UserSystemRoles
{
    public class UserSystemRole: IdentityUserRole<Guid>
    {
        public UserSystemRole() : base() { }

        public User User { get; set; }

        public SystemRole SystemRole { get; set; }
    }
}
