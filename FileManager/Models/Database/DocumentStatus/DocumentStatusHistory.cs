using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.UserDepartmentRoles;

namespace FileManager.Models.Database.DocumentStatus
{
    public class DocumentStatusHistory
    {
        public DocumentStatusHistory(Guid newStatusId,string comment, Guid departamentDocumentId, DateTime settingDateTime)
        { 
            Id = Guid.NewGuid();
            CommentEdits = comment;
            DocumentStatusId = newStatusId;
            DepartmentsDocumentId = departamentDocumentId;
            SettingDateTime = settingDateTime;

        }
        public DocumentStatusHistory()
        {

        }
        public Guid Id { get; set; }
        public string CommentEdits { get; set; }

        public Guid DocumentStatusId { get; set; }
        public DocumentStatus DocumentStatus { get; set; }

        public DateTime SettingDateTime { get; set; }

        public Guid DepartmentsDocumentId { get; set; }
        public DepartmentsDocument DepartmentsDocument { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }


    }
}
