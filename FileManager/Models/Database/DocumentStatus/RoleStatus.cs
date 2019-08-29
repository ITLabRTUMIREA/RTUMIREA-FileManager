using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.UserDepartmentRoles;

namespace FileManager.Models.Database.DocumentStatus
{
    public class RoleStatus
    {
        public Guid Id { get; set; }

        public Guid DocumentStatusId { get; set; }
        public DocumentStatus DocumentStatus { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
