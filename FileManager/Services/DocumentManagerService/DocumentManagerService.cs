using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.DocumentStatuses;
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
        public async Task<int> GetCountOfSetDocWithCertainStatus(Guid userId, string status)
        {
            _logger.LogWarning("using danger code");

            var list = await db.DocumentStatusHistory
               .Include(dsh => dsh.DocumentStatus)
               .Where(dsh => dsh.UserId == userId
                   && dsh.DocumentStatus.Id == (db.DepartmentsDocument.FirstOrDefault(d => d.Id == dsh.DepartmentsDocumentId)).LastSetDocumentStatusId
                   && dsh.DocumentStatus.Status == status)
               .GroupBy(dsh => dsh.DepartmentsDocumentId)
               .ToListAsync();

            return list.Count;
        }
        public async Task<List<DepartmentsDocument>> GetDocsWithCertainStatus(Guid userId, string status)
        {
            _logger.LogWarning("using danger code");

            var listOfDocs = await db.DocumentStatusHistory
               .Include(dsh => dsh.DocumentStatus)
               .Where(dsh => dsh.UserId == userId
                   && dsh.DocumentStatus.Id == (db.DepartmentsDocument.FirstOrDefault(d => d.Id == dsh.DepartmentsDocumentId)).LastSetDocumentStatusId
                   && dsh.DocumentStatus.Status == status)
               .GroupBy(dsh => dsh.DepartmentsDocumentId)
               .ToListAsync();

            List<DepartmentsDocument> departmentsDocuments = new List<DepartmentsDocument>();
            foreach (var item in listOfDocs)
            {
                var a = item.Key;
                var b = await db.DepartmentsDocument
                    .Include(dd => dd.ReportingYearDocumentTitle)
                        .ThenInclude(ry => ry.DocumentTitle)
                            .ThenInclude(ry => ry.DocumentType)
                    .Include(dd => dd.ReportingYearDocumentTitle)
                        .ThenInclude(ry => ry.ReportingYear)
                    .Include(dd => dd.Department)
                    .FirstOrDefaultAsync(dd => dd.Id == a);
                departmentsDocuments.Add(b);
            }

            return departmentsDocuments;
        }
    }
}
