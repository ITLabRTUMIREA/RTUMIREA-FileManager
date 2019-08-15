using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly RoleManager<Role> _roleManager;
        private readonly FileManagerContext db;

        public User currentUser;

        public List<IGrouping<string, UserDepartmentRole>> allUserDepartmentRoles;


        public UserInfoModel(UserManager<User> userManager,
            FileManagerContext context,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            db = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                //
                // Without this string code doesn't connecting with Role Entity
                _roleManager.Roles.ToList();
                //
                currentUser =  await _userManager.GetUserAsync(HttpContext.User);

                if (currentUser != null)
                {

                    allUserDepartmentRoles = await db.UserRoleDepartment
                        .Where(urd => urd.UserId.Equals(currentUser.Id))
                        .GroupBy(urd => urd.Department.Name)
                        .ToListAsync();
                    return Page();
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
