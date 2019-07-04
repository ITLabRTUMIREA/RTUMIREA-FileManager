using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileManager.Pages.Account.Departments
{
    public class CreateDepartmentModel : PageModel
        // TODO Make departaments managing 
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
                Departament newDepartment = new Departament();
                newDepartment.Name = name;

                await db.Departament.AddAsync(newDepartment);
                await db.SaveChangesAsync();

                return RedirectToPage("DepartmentsList");
                 
                
            }
            return Page();
        }
    }
}