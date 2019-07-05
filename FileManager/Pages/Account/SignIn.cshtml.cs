using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FileManager.Models;
using FileManager.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using FileManager.Models.ViewModels.Account;
using FileManager.Models.Database.UserRole;

namespace FileManager.Pages.SignIn
{
    public class SignInModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public SignInModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SignInViewModel SignInViewModel { get; set; }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByEmailAsync(SignInViewModel.Email) != null)
                    {
                        User user = await _userManager.FindByEmailAsync(SignInViewModel.Email);
                        var result = await _signInManager.PasswordSignInAsync(user, SignInViewModel.Password, true, false);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, true);
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            if (!await _userManager.IsEmailConfirmedAsync(user))
                            {
                                ModelState.AddModelError(string.Empty, "Почта не подтверждена");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Неверный Email или пароль");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Неверный Email или пароль");
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