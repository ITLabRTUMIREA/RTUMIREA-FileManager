﻿using System;
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
using System.Text.Encodings.Web;
using FileManager.Models.EmailSendingOptions;
using FileManager.Services.EmailConfirmationService;
using Microsoft.Extensions.Options;
using FileManager.Models.ViewModels.Account;

namespace FileManager.Pages.SignUp
{
    public class SignUpModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly IMapper _mapper;

        public SignUpModel(UserManager<User> userManager,
            IEmailConfirmationService emailConfirmationService,
            IMapper mapper)
        {
            _userManager = userManager;
            _emailConfirmationService = emailConfirmationService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; }

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

                        User user = _mapper.Map<SignUpViewModel, User>(SignUpViewModel);

                        var result = await _userManager.CreateAsync(user, SignUpViewModel.Password);

                        string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                        string confirmationLink = Url.Action("ConfirmEmail",
                            "EmailConfirmation",
                            new { userid = user.Id, token = confirmationToken },
                            protocol: HttpContext.Request.Scheme);

                        await _emailConfirmationService.SendEmailConfirmationAsync(user,confirmationLink);

                        if (result.Succeeded)
                        {
                            return Content("Confirm email please. Your email: "+ user.Email);
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