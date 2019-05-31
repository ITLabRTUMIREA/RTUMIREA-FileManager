using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class DocumentStatus
    {
        public Guid ID { get; set; }
        public string Status { get; set; }

        public List<DocumentStatusHistory> DocumentStatusHistories { get; set; }

        public List<RoleStatus> RoleStatuses { get; set; }

    }
}
