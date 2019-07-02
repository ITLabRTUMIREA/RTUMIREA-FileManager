using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileManager.Pages.Account
{
    public class ResetPasswordConfirmationModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordConfirmationModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return RedirectToRoute("/");
        }

        [BindProperty] public ResetPasswordConfirmationViewModel ResetPasswordConfirmationViewModel { get; set; }

        public IActionResult OnGetResetPassword(Guid userId, string token)
        {
            new ResetPasswordConfirmationViewModel(userId, token);
            return Page();
        }

        public async Task<IActionResult> OnPostResetPassword()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByEmailAsync(ResetPasswordConfirmationViewModel.Email);

                    if (user.Id != ResetPasswordConfirmationViewModel.UserId)
                    {
                        ModelState.AddModelError("ResetPasswordConfirmationViewModel.Email",
                            "Email не соответствует вашему аккаунту");
                    }
                    else
                    {
                        IdentityResult result = await _userManager.ResetPasswordAsync(user,
                            ResetPasswordConfirmationViewModel.Token,
                            ResetPasswordConfirmationViewModel.Password);
                        if (result.Succeeded)
                        {
                            return Content("ResetPasswordConfirmation is succeeded");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return Page();
        }
    }
}