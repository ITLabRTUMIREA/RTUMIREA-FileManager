using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.FileManagerService
{
    public class FileManagerService : IFileManagerService
    {
        private readonly FileManagerContext db;
        private readonly ILogger<FileManagerService> _logger;
        private readonly IHostingEnvironment _appEnvironment;

        public FileManagerService(
            FileManagerContext context,
            ILogger<FileManagerService> logger,
            IHostingEnvironment appEnvironment)
        {
            db = context;
            _logger = logger;
            _appEnvironment = appEnvironment;
        }
        public async Task<int> UploadFileAsync(IFormFile uploadedFile,
            Guid yearId,
            Guid departmentId,
            Guid documentTitleId)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {

                    await uploadedFile.OpenReadStream().CopyToAsync(fileStream);
                }

                Guid reportingYearDocumentTitleId = await GetCurrentReportingYearDocumentTitleId(yearId, documentTitleId);

                if (reportingYearDocumentTitleId != null)
                {

                    DepartmentsDocument departmentsDocument = await GetDepartmentsDocument(departmentId, reportingYearDocumentTitleId);

                    return await SaveDocumentPathAsync(departmentsDocument.Id, uploadedFile.FileName, path);
                }
            }
            return -1;
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
                departmentsDocument = await db.DepartmentsDocument.FirstOrDefaultAsync(dd => dd.DepartmentId == departmentId
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

        public async Task<int> SaveDocumentPathAsync(Guid departmentsDocumentId, string FileName, string path)
        {

            await db.DepartmentsDocumentsVersion
                .AddAsync(new DepartmentsDocumentsVersion(departmentsDocumentId, FileName, path, DateTime.UtcNow));

            return await db.SaveChangesAsync();
        }
    }
}
