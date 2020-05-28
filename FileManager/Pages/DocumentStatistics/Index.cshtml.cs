using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileManager.Services.DocumentManagerService;
using FileManager.Models.Database.DocumentStatuses;
using Microsoft.Extensions.Logging;
using FileManager.Services.GetAccountDataService;
using FileManager.Models.Database.DepartmentsDocuments;

namespace FileManager.Pages.DocumentStatistics
{
    public class DocumentStatisticsModel : PageModel
    {
        //private readonly FileManagerContext db;
        private readonly ILogger<DocumentStatisticsModel> _logger;
        //private readonly ISmartBreadcrumbService _breadcrumbService;
        private readonly IGetAccountDataService _getAccountDataService;
        //private readonly IFileManagerService _fileManagerService;
        private readonly IDocumentManagerService _documentManagerService;

        //public DocumentTitle DocumentsTitle;
        //public List<DepartmentsDocumentsVersion> UploadedDocuments;
        //public List<DocumentStatusHistory> DocumentStatusHistories;
        //public User user;
        //public bool IsUserTheChecker = false;
        //public string actualDocumentStatus;
        //public List<DocumentStatus> AllAvailabledocumentStatuses;
        //public DepartmentsDocument departmentsDocument;
        public Guid NewDocumentStatus;

        public Guid selectedReportingYearId;
        public Guid selectedDepartmentId;
        public Guid selectedDocumentTypeId;
        public Guid selectedDocumentTitleId;

        public List<DepartmentsDocument> AcceptedDocs;
        public List<DepartmentsDocument> WarningDocs;
        public List<DepartmentsDocument> NotCheckedDocs;

        public DocumentStatisticsModel(
            //FileManagerContext context,
            ILogger<DocumentStatisticsModel> logger,
            //ISmartBreadcrumbService breadcrumbService,
            IGetAccountDataService getAccountDataService,
            //IFileManagerService fileManagerService,
            IDocumentManagerService documentManagerService)
        {
            //db = context;
            _logger = logger;
            //_breadcrumbService = breadcrumbService;
            _getAccountDataService = getAccountDataService;
            //_fileManagerService = fileManagerService;
            _documentManagerService = documentManagerService;

        }
        public async Task OnGet()
        {
            var userId = (await _getAccountDataService.GetCurrentUser()).Id;
            AcceptedDocs = await _documentManagerService
                .GetDocsWithCertainStatus(userId, "Принято");
            WarningDocs = await _documentManagerService
                .GetDocsWithCertainStatus(userId, "Нуждается в доработке");
            NotCheckedDocs = await _documentManagerService
                .GetDocsWithCertainStatus(userId, "Не проверено");


        }
    }
}