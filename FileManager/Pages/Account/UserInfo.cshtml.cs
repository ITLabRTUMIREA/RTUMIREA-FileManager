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
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Account
{
    public class UserInfoModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly FileManagerContext db;
        private readonly ILogger<UserInfoModel> _logger;

        public User currentUser;
        public bool IsSystemAdmin = false;

        public List<IGrouping<string, UserDepartmentRole>> allUserDepartmentRoles;


        public UserInfoModel(UserManager<User> userManager,
            FileManagerContext context,
            ILogger<UserInfoModel> logger)
        {
            _userManager = userManager;
            db = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                currentUser =  await _userManager.GetUserAsync(HttpContext.User);

                IsSystemAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "SystemAdmin");

                if (currentUser != null)
                {

                    allUserDepartmentRoles = await db.UserRoleDepartment
                        .Where(urd => urd.UserId.Equals(currentUser.Id))
                        .Include(urd => urd.Role)
                        .GroupBy(urd => urd.Department.Name)
                        .ToListAsync();
                    return Page();
                }
                return Page();
            }catch (Exception e)
            {
                _logger.LogError(e, "Error while getting user info");
                return NotFound();
       
            }
        }
    }
}
