using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.ViewModels.Account
{
    public class ResetPasswordConfirmationViewModel
    {
        public ResetPasswordConfirmationViewModel(Guid userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
        }
        public Guid UserId { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]

        [EmailAddress(ErrorMessage = "Неверный Email адрес.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[A-Z])(?=.*?[a-z])" +
                           "(?=.*?[^a-zA-Z0-9])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])" +
                           "(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}" +
                           "$", ErrorMessage = "Пароль должен быть из не менее 8 символов и содержать " +
                                               "следующее: прописные символы (A-Z), строчные символы (a-z)," +
                                               " цифры (0-9) и специальные символы (например: !@#$%^&*).")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}
