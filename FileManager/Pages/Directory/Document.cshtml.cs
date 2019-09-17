using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.ReportingYearDocumentTitles;
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
        private readonly IHostingEnvironment _appEnvironment;

        public DocumentTitle DocumentsTitle;

        public Guid selectedReportingYearId;
        public Guid selectedDepartmentId;
        public Guid selectedDocumentTypeId;
        public Guid selectedDocumentTitleId;


        public DocumentModel(FileManagerContext context,
            ILogger<DocumentModel> logger,
            ISmartBreadcrumbService breadcrumbService,
            IGetAccountDataService getAccountDataService,
            IHostingEnvironment appEnvironment)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
            _getAccountDataService = getAccountDataService;
            _appEnvironment = appEnvironment;
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

                ViewData["BreadcrumbNode"] = await _breadcrumbService.GetDocumentBreadCrumbNodeAsync(
                    yearId,
                    departmentId,
                    documentTypeId,
                    documentTitleId);


                DocumentsTitle = await db.DocumentTitle.FirstOrDefaultAsync(dt => dt.Id.Equals(documentTitleId));


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
                    // путь к папке Files
                    string path = "/Files/" + uploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    Guid reportingYearDocumentTitleId = (await db.ReportingYearDocumentTitle
                        .FirstOrDefaultAsync(rydt => rydt.ReportingYearId == yearId
                            && rydt.DocumentTitleId == documentTitleId)
                        ).Id;

                    if (reportingYearDocumentTitleId != null)
                    {

                        DepartmentsDocument departmentsDocument = await GetDepartmentsDocument(departmentId, reportingYearDocumentTitleId);

                        if (await SaveDocumentPath(departmentsDocument.Id, uploadedFile.FileName, path) > 0)
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
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving document");
                return NotFound();
            }
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

        public async Task<int> SaveDocumentPath(Guid departmentsDocumentId, string FileName, string path)
        {
            DepartmentsDocumentsVersion departmentsDocumentsVersion;
            if (await db.DepartmentsDocumentsVersion
                .FirstOrDefaultAsync(ddv => ddv.DepartmentDocumentId == departmentsDocumentId) != null)
            {
                int lastVersion = await db.DepartmentsDocumentsVersion
                    .Where(ddv => ddv.DepartmentDocumentId == departmentsDocumentId)
                    .Select(ddv => ddv.Version)
                    .MaxAsync();
                departmentsDocumentsVersion = new DepartmentsDocumentsVersion(departmentsDocumentId, FileName, path, lastVersion);
            }
            else
            {
                departmentsDocumentsVersion = new DepartmentsDocumentsVersion(departmentsDocumentId, FileName, path);
            }
            await db.DepartmentsDocumentsVersion.AddAsync(departmentsDocumentsVersion);

            return await db.SaveChangesAsync();
        }
    }
}