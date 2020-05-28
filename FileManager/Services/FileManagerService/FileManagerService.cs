using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Services.DocumentManagerService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileManager.Services.FileManagerService
{
    public class FileManagerService : IFileManagerService
    {
        private readonly FileManagerContext db;
        private readonly ILogger<FileManagerService> _logger;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IDocumentManagerService _documentManagerService;

        public FileManagerService(
            FileManagerContext context,
            ILogger<FileManagerService> logger,
            IHostingEnvironment appEnvironment,
            IDocumentManagerService documentManagerService)
        {
            db = context;
            _logger = logger;
            _appEnvironment = appEnvironment;
            _documentManagerService = documentManagerService;

            InitializeFilesDirectory();


        }

        public void InitializeFilesDirectory()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Files"));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
        public async Task<int> UploadFileAsync(IFormFile uploadedFile,
            Guid yearId,
            Guid departmentId,
            Guid documentTitleId,
            Guid userId)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files"), DateTime.Now.Ticks.ToString() + uploadedFile.FileName);
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(path, FileMode.Create))
                {

                    await uploadedFile.OpenReadStream().CopyToAsync(fileStream);
                }

                Guid reportingYearDocumentTitleId = await _documentManagerService.GetCurrentReportingYearDocumentTitleId(yearId, documentTitleId);

                if (reportingYearDocumentTitleId != null)
                {

                    DepartmentsDocument departmentsDocument = await _documentManagerService.GetDepartmentsDocument(departmentId, reportingYearDocumentTitleId);

                    return await SaveDocumentPathAsync(departmentsDocument.Id, uploadedFile.FileName, path, userId);
                }
            }
            return -1;
        }



        public async Task<int> SaveDocumentPathAsync(Guid departmentsDocumentId, string FileName, string path, Guid userId)
        {

            await db.DepartmentsDocumentsVersion
                .AddAsync(new DepartmentsDocumentsVersion(departmentsDocumentId, FileName, path, DateTime.UtcNow, userId));

            return await db.SaveChangesAsync();
        }
    }
}
