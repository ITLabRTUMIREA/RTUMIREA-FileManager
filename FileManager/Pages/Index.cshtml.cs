using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.YearDocumentTitles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace FileManager.Pages
{
    [DefaultBreadcrumb("Index Page")]
    public class IndexModel : PageModel
    {
        private readonly FileManagerContext db;
        public List<Year> Years;

        public IndexModel(FileManagerContext context)
        {
            db = context;
        }
        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/SignIn");
            }
            else
            {
                Years = db.Year.ToList();

                return Page();
            }
        }
    }
}
