using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Account
{
    public class UserInfoModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly FileManagerContext db;

        public User currentUser { get; set; }

        public List<IGrouping<string, UserDepartmentRole>> allUserDepartmentRoles { get; set; }


        public UserInfoModel(UserManager<User> userManager,
            FileManagerContext context)
        {
            _userManager = userManager;
            db = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // TODO Fix null reference error
                currentUser = await _userManager.GetUserAsync(HttpContext.User);

                if (currentUser != null)
                {

                    allUserDepartmentRoles = await db.UserRoleDepartment
                        .Where(urd => urd.UserId.Equals(currentUser.Id))
                        .GroupBy(urd => urd.Department.Name)
                        .ToListAsync();
                }
                return Page();
            }catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }
    }
}
