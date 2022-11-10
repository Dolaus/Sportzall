﻿using System.ComponentModel.DataAnnotations;

namespace Sportzall.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указан Ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
