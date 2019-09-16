using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.ReportingYearDocumentTitles
{
    public class ReportingYear
    {
        public ReportingYear() { }
        public ReportingYear(int number)
        {
            Id = Guid.NewGuid();
            Number = number;
        }
        public Guid Id { get; set; }
        public int Number { get; set; }

        public List<ReportingYearDocumentTitle> ReportingYearDocumentTitles { get; set; }
    }
}
