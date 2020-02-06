using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.GetSummaryOfUploadedFilesAndChecks
{
    public class SummaryOfUploadedFilesAndChecks
    {
        public SummaryOfUploadedFilesAndChecks(string info, DateTime dateTime)
        {
            Info = info;
            DateTime = dateTime;
        }
        public string Info { get; set; }
        public DateTime DateTime { get; set; }
    }
}
