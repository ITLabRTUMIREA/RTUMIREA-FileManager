using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Account.Departments
{
    public class DepartmentsListModel : PageModel
        // TODO Make Departments managing
        // TODO Change all names Departament to Department
    {
        private readonly FileManagerContext db;

        public DepartmentsListModel(FileManagerContext context)
        {
            db = context;
        }

        public List<Department> Departments;

        public async Task<IActionResult> OnGet()
        {
            Departments = await db.Department.ToListAsync<Department>();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(String id)
        {
            if (id.Equals(null))
            {
                return Page();
            }

            Department Department = await db.Department.FirstOrDefaultAsync<Department>(d => d.ID.ToString() == id);

            if (Department != null)
            {
                db.Department.Remove(Department);
                await db.SaveChangesAsync();
            }
            return RedirectToPage("/Account/UserRoleDepartmentManagement/Departments/DepartmentsList");
        }
    }
}