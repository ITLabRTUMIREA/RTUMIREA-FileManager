using FileManager.Models.Database.DepartmentsDocuments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.FileManagerService
{
    public interface IFileManagerService
    {
        Task<int> UploadFileAsync(IFormFile uploadedFile,
            Guid yearId,
            Guid departmentId,
            Guid documentTitleId);



        Task<int> SaveDocumentPathAsync(Guid departmentsDocumentId, string FileName, string path);
    }
}
