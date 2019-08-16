using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.UserDepartmentRoles
{
    public class UserRole: IdentityUserRole<Guid>
    {
        public UserRole() : base() { }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
