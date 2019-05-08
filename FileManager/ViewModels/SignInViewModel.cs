using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.ViewModels
{
    public class SignInViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Обязательное поле.")]
        [EmailAddress(ErrorMessage = "Неверный Email адрес.")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Password { get; set; }

    }
}
