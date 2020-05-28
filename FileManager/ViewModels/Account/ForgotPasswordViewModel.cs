using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Обязательное поле.")]
        [EmailAddress(ErrorMessage = "Неверный Email адрес.")]
        public string Email { get; set; }
    }
}
