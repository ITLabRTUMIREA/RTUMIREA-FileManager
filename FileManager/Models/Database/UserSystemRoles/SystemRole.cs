using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.UserSystemRoles
{
    public class SystemRole : IdentityRole<Guid>
    {
        public SystemRole(string name) : base(name)
        {
        }

        public List<UserSystemRole> UserSystemRoles { get; set; }
    }
}