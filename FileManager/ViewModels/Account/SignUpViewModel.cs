using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models.ViewModels.Account
{
    public class SignUpViewModel
    {

        [Required(ErrorMessage = "Обязательное поле.")]
        [EmailAddress(ErrorMessage = "Неверный Email адрес.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        public string FistName { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

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
