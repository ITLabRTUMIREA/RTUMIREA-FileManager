using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.ViewModels.Account;
using FileManager.Services.ResetPasswordService;
using FileManager.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileManager.Pages.Account.ForgotPasswordPage
{
    public class ForgotPasswordPageModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IResetPasswordService _resetPasswordService;
        public ForgotPasswordPageModel(UserManager<User> userManager,IResetPasswordService resetPasswordService)
        {
            _userManager = userManager;
            _resetPasswordService = resetPasswordService;
        }
        [BindProperty] public ForgotPasswordViewModel ForgotPasswordViewModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByEmailAsync(ForgotPasswordViewModel.Email);
                    if (user != null && (await _userManager.IsEmailConfirmedAsync(user)))
                    {
                        string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                        string resetPasswordCallbackLink = Url.Page("ResetPasswordConfirmation",
                            "Account",
                            new {userId = user.Id, token = resetPasswordToken},
                            protocol: HttpContext.Request.Scheme);

                        await _resetPasswordService.SendResetPasswordConfirmationLinkToEmailAsync(user, resetPasswordCallbackLink);
                        // TODO To Finish Forgot password page
                        // TODO To Finish Reseting password completely


                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Неверный Email или он не подтверждён");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверный Email");
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