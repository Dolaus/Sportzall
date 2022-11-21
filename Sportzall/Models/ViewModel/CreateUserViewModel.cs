using System.ComponentModel.DataAnnotations;

namespace Sportzall.Models.ViewModel
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Range(1940, 2022, ErrorMessage = "Неправильний рік")]
        public int? Year { get; set; }
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ShortInfo { get; set; }
        public Role? Role { get; set; }
    }
}
