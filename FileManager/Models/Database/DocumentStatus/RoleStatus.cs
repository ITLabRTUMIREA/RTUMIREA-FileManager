using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class RoleStatus
    {
        public Guid ID { get; set; }

        public Guid DocumentStatusID { get; set; }
        public DocumentStatus DocumentStatus { get; set; }

        public Guid RoleID { get; set; }
        public Role Role { get; set; }
    }
}
