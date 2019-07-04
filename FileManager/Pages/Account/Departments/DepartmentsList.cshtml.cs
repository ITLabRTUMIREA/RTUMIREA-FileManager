using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Account.Departments
{
    public class DepartmentsListModel : PageModel
        // TODO Make departaments managing
        // TODO Change all names Departament to Department
    {
        private readonly FileManagerContext db;

        public DepartmentsListModel(FileManagerContext context)
        {
            db = context;
        }

        public List<Departament> Departaments;

        public async Task<IActionResult> OnGet()
        {
            Departaments = await db.Departament.ToListAsync<Departament>();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(String id)
        {
            if (id.Equals(null))
            {
                return Page();
            }

            Departament departament = await db.Departament.FirstOrDefaultAsync<Departament>(d => d.ID.ToString() == id);

            if (departament != null)
            {
                db.Departament.Remove(departament);
                await db.SaveChangesAsync();
            }
            return RedirectToPage("/Account/Departments/DepartmentsList");
        }
    }
}