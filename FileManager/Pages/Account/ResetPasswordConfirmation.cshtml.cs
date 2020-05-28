using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Account
{
    public class ResetPasswordConfirmationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ResetPasswordConfirmationModel> _logger;

        [BindProperty]
        public ResetPasswordConfirmationViewModel ResetPasswordConfirmationViewModel { get; set; }

        [TempData]
        public Guid userId { get; set; }

        [TempData]
        public string resetPasswordConfirmationToken { get; set; }

        public ResetPasswordConfirmationModel(UserManager<User> userManager,
            ILogger<ResetPasswordConfirmationModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            try
            {
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page CreateDepartment");
                return NotFound();
            }
        }



        public IActionResult OnGetResetPassword(Guid userId, string token)
        {
            try
            {
                this.userId = userId;
                resetPasswordConfirmationToken = token;
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while reseting password");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByIdAsync(ResetPasswordConfirmationViewModel.UserId.ToString());

                    if (user != null)
                    {
                        IdentityResult result = await _userManager.ResetPasswordAsync(user,
                            ResetPasswordConfirmationViewModel.ResetPasswordConfirmationToken,
                            ResetPasswordConfirmationViewModel.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToPagePermanent("Info",
                                "GetInfoMessage",
                                new { message = "Пароль успешно изменён" });
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return Page();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return Page();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while reseting password");
                return NotFound();
            }
        }
    }
}
