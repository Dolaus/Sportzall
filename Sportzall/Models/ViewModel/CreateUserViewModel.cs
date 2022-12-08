using System.ComponentModel.DataAnnotations;

namespace Sportzall.Models.ViewModel
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        [MinLength(1), DataType(DataType.EmailAddress), EmailAddress, MaxLength(50), Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Пароль має містити неменше 4 символів")]
        public string Password { get; set; }
        [Range(1940, 2022, ErrorMessage = "Неправильний рік")]
        public int? Year { get; set; }
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ShortInfo { get; set; }
        public int? ChessPress { get; set; }
        public int? BenchPress { get; set; }
        public int? Squat { get; set; }
        public Role? Role { get; set; }
    }
}
