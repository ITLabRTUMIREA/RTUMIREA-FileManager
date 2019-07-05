using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.Database.YearDocumentTitles
{
    public class Year
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<YearDocumentTitle> YearDocumentTitles { get; set; }
    }
}
