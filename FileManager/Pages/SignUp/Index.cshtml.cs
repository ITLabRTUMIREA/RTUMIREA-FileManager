using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FileManager.Models;
using FileManager.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace FileManager.Pages.SignUp
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel (UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SignUpViewModel SignUpViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByEmailAsync(SignUpViewModel.Email) != null)
                    {
                        ModelState.AddModelError("SignUpViewModel.Email", "Пользователь с данным Email уже существует");
                    }
                    else
                    {
                        Mapper.Initialize(cfg => cfg.CreateMap<SignUpViewModel, User>()
                            .ForMember("UserName", opt => opt.MapFrom(src => src.Email)));
                        User user = Mapper.Map<SignUpViewModel, User>(SignUpViewModel);
                        var result = await _userManager.CreateAsync(user, SignUpViewModel.Password);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToPage("../Index");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return Page();
        }
    }
}