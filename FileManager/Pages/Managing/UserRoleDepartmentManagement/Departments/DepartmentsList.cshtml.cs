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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Managing.UserRoleDepartmentManagement.Departments
{
    public class DepartmentsListModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<DepartmentsListModel> _logger;
        private readonly IGetAccountDataService _getAccountDataService;

        public List<Department> Departments;

        public DepartmentsListModel(FileManagerContext context,
             ILogger<DepartmentsListModel> logger,
             IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _getAccountDataService = getAccountDataService;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    Departments = await db.Department.ToListAsync<Department>();

                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page DepartmentList");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    if (id.Equals(null))
                    {
                        return Page();
                    }

                    Department Department = await db.Department.FirstOrDefaultAsync<Department>(d => d.Id.ToString() == id);

                    if (Department != null)
                    {
                        db.Department.Remove(Department);
                        await db.SaveChangesAsync();
                    }
                    return RedirectToPage("/Managing/UserRoleDepartmentManagement/Departments/DepartmentsList");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting department");
                return NotFound();
            }
        }
    }
}