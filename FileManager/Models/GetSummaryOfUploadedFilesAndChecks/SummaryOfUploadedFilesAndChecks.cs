using FileManager.Models.Database.UserDepartmentRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.GetSummaryOfUploadedFilesAndChecks
{
    public class SummaryOfUploadedFilesAndChecks
    {
        public SummaryOfUploadedFilesAndChecks(string info, DateTime dateTime, string uploadedUser)
        {
            Info = info;
            DateTime = dateTime;
            UploadedUser = uploadedUser;
        }
        public string Info { get; set; }
        public DateTime DateTime { get; set; }

        public string UploadedUser;

    }
}
