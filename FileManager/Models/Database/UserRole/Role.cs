using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.DocumentStatus;
using Microsoft.AspNetCore.Identity;

namespace FileManager.Models.Database.UserRole
{
    public class Role:IdentityRole<Guid>
    {
        public List<RoleStatus> RoleStatuses { get; set; }

        public Role(string name):base(name)
        {
        }
    }
}
