using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.DocumentStatus
{
    public class DocumentStatus
    {
        public DocumentStatus(string Status)
        {
            this.Id = Guid.NewGuid();
            this.Status = Status;
        }
        public Guid Id { get; set; }
        public string Status { get; set; }

        public List<DocumentStatusHistory> DocumentStatusHistories { get; set; }

        public List<RoleStatus> RoleStatuses { get; set; }

    }
}
