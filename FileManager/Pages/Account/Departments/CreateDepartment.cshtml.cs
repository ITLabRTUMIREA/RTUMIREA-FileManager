using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileManager.Pages.Account.Departments
{
    public class CreateDepartmentModel : PageModel
        // TODO Make Departments managing 
    {
        private readonly FileManagerContext db;
        public CreateDepartmentModel(FileManagerContext context)
        {
            db = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name)
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
    }
}