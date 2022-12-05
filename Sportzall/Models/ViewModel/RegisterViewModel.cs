using System.ComponentModel.DataAnnotations;

namespace Sportzall.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не вказано Ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        [MinLength(1), DataType(DataType.EmailAddress), EmailAddress, MaxLength(50), Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Пароль має містити неменше 4 символів")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        
        [Compare("Password", ErrorMessage = "Пароль введено невірно")]
        public string ConfirmPassword { get; set; }
    }
}
