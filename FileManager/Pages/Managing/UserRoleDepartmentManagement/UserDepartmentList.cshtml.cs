using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Services.GetAccountDataService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Managing.UserRoleDepartmentManagement
{
    public class UserDepartmentListModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IGetAccountDataService _getAccountDataService;
        private readonly ILogger<UserDepartmentListModel> _logger;

        public bool IsSystemAdmin = false;
        public List<User> Users;

        public UserDepartmentListModel (UserManager<User> userManager,
            IGetAccountDataService getAccountDataService,
            ILogger<UserDepartmentListModel> logger)
        {
            _userManager = userManager;
            _getAccountDataService = getAccountDataService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                IsSystemAdmin = _getAccountDataService.IsSystemAdmin();

                if (IsSystemAdmin || await _getAccountDataService.IsAdminOnAnyDepartment())
                {
                    Users = await _userManager.Users.ToListAsync();

                    return Page();
                }else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page UserDepartmentList");
                return NotFound();
            }
        }
    }
}