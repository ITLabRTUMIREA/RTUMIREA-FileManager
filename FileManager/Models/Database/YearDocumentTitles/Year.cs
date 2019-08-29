using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.YearDocumentTitles
{
    public class Year
    {
        public Guid Id { get; set; }
        public int Number { get; set; }

        public List<YearDocumentTitle> YearDocumentTitles { get; set; }
    }
}
