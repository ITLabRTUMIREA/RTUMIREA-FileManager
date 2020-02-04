using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.DocumentManagerService
{
    public class DocumentManagerService : IDocumentManagerService
    {
        private readonly FileManagerContext db;
        private readonly ILogger<DocumentManagerService> _logger;

        public DocumentManagerService(FileManagerContext context, ILogger<DocumentManagerService> logger)
        {
            db = context;
            _logger = logger;
        }

        public async Task<Guid> GetCurrentReportingYearDocumentTitleId(Guid yearId, Guid documentTitleId)
        {
            return (await db.ReportingYearDocumentTitle
                    .FirstOrDefaultAsync(rydt => rydt.ReportingYearId == yearId
                        && rydt.DocumentTitleId == documentTitleId)
                    ).Id;
        }

        public async Task<DepartmentsDocument> GetDepartmentsDocument(Guid departmentId, Guid reportingYearDocumentTitleId)
        {
            DepartmentsDocument departmentsDocument;

            if (await db.DepartmentsDocument.FirstOrDefaultAsync(dd => dd.DepartmentId == departmentId
                    && dd.ReportingYearDocumentTitleId == reportingYearDocumentTitleId) != null)
            {
                departmentsDocument = await db.DepartmentsDocument
                    .FirstOrDefaultAsync(dd => dd.DepartmentId == departmentId
                    && dd.ReportingYearDocumentTitleId == reportingYearDocumentTitleId);
            }
            else
            {
                departmentsDocument = new DepartmentsDocument(departmentId, reportingYearDocumentTitleId);

                await db.DepartmentsDocument.AddAsync(departmentsDocument);

                await db.SaveChangesAsync();
            }
            return departmentsDocument;
        }
    }
}
