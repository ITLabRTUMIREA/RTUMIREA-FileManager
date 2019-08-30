using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Services.GetAccountDataService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Managing.UserRoleDepartmentManagement.Departments
{
    public class CreateDepartmentModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly IGetAccountDataService _getAccountDataService;
        private readonly ILogger<CreateDepartmentModel> _logger;


        public CreateDepartmentModel(FileManagerContext context,
            IGetAccountDataService getAccountDataService,
            ILogger<CreateDepartmentModel> logger)
        {
            db = context;
            _getAccountDataService = getAccountDataService;
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page CreateDepartment");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        Department newDepartment = new Department();
                        newDepartment.Name = name;

                        await db.Department.AddAsync(newDepartment);
                        await db.SaveChangesAsync();

                        return RedirectToPage("DepartmentsList");


                    }
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while creating new department");
                return NotFound();
            }
        }
    }
}