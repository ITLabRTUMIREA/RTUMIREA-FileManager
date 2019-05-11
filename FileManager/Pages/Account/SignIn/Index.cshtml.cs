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

namespace FileManager.Pages.SignIn
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public IndexModel(UserManager<User> userManager, SignInManager<User> signInManager)
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByEmailAsync(SignInViewModel.Email) != null)
                    {
                        User user = await _userManager.FindByEmailAsync(SignInViewModel.Email);
                        var result = await _signInManager.CheckPasswordSignInAsync(user, SignInViewModel.Password, false);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Неверный Email или пароль");
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