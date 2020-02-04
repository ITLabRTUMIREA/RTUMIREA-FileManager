using FileManager.Models.Database.DepartmentsDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.DocumentManagerService
{
    public interface IDocumentManagerService
    {
        Task<DepartmentsDocument> GetDepartmentsDocument(Guid departmentId, Guid reportingYearDocumentTitleId);

        Task<Guid> GetCurrentReportingYearDocumentTitleId(Guid yearId, Guid documentTitleId);
    }
}
