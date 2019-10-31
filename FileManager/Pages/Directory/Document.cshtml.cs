using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Services.FileManagerService;
using FileManager.Services.GetAccountDataService;
using FileManager.Services.SmartBreadcrumbService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Directory
{
    public class DocumentModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<DocumentModel> _logger;
        private readonly ISmartBreadcrumbService _breadcrumbService;
        private readonly IGetAccountDataService _getAccountDataService;
        private readonly IFileManagerService _fileManagerService;

        public DocumentTitle DocumentsTitle;
        public List<DepartmentsDocumentsVersion> UploadedDocuments;

        public Guid selectedReportingYearId;
        public Guid selectedDepartmentId;
        public Guid selectedDocumentTypeId;
        public Guid selectedDocumentTitleId;


        public DocumentModel(FileManagerContext context,
            ILogger<DocumentModel> logger,
            ISmartBreadcrumbService breadcrumbService,
            IGetAccountDataService getAccountDataService,
            IFileManagerService fileManagerService)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
            _getAccountDataService = getAccountDataService;
            _fileManagerService = fileManagerService;
        }
        public async Task<IActionResult> OnGetAsync(Guid yearId, Guid departmentId, Guid documentTypeId, Guid documentTitleId)
        {
            try
            {
                if (!await _getAccountDataService.UserIsHaveAnyRoleOnDepartment(departmentId))
                {
                    return NotFound();
                }
                selectedReportingYearId = yearId;
                selectedDepartmentId = departmentId;
                selectedDocumentTypeId = documentTypeId;
                selectedDocumentTitleId = documentTitleId;

                DocumentsTitle = await db.DocumentTitle.FirstOrDefaultAsync(dt => dt.Id.Equals(documentTitleId));

                DepartmentsDocument departmentsDocument = await _fileManagerService
                    .GetDepartmentsDocument(departmentId,
                        await _fileManagerService.GetCurrentReportingYearDocumentTitleId(yearId, documentTitleId));


                UploadedDocuments = await db.DepartmentsDocumentsVersion
                    .Where(ddv => ddv.DepartmentDocumentId == departmentsDocument.Id)
                    .OrderByDescending(ddv => ddv.UploadedDateTime)
                    .ToListAsync();

                ViewData["BreadcrumbNode"] = await _breadcrumbService.GetDocumentBreadCrumbNodeAsync(
                    yearId,
                    departmentId,
                    documentTypeId,
                    documentTitleId);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting document page");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostSaveFileAsync(IFormFile uploadedFile,
            Guid yearId,
            Guid departmentId,
            Guid documentTypeId,
            Guid documentTitleId)
        {
            try
            {
                if (uploadedFile != null)
                {

                    if (await _fileManagerService.UploadFileAsync(
                            uploadedFile,
                            yearId,
                            departmentId,
                            documentTitleId) > 0)
                    {
                        return RedirectToPage("Document", routeValues: new
                        {
                            yearId,
                            departmentId,
                            documentTypeId,
                            documentTitleId
                        });
                    }

                }
                return NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving document");
                return NotFound();
            }
        }

        public async Task<VirtualFileResult> OnGetDownloadDocumentAsync(Guid departmentsDocumentsVersionId)
        {
            DepartmentsDocumentsVersion departmentsDocumentsVersion = await db.DepartmentsDocumentsVersion
                .FirstOrDefaultAsync(ddv => ddv.Id == departmentsDocumentsVersionId);
            var filepath = Path.Combine("~/Files", departmentsDocumentsVersion.FileName);

            return File(filepath, "application/octet-stream", departmentsDocumentsVersion.FileName);
        }
    }
}