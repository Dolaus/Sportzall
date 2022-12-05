using System.ComponentModel.DataAnnotations;

namespace Sportzall.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(1940,2022,ErrorMessage ="Неправильний рік")]
        public int? Year { get; set; }
        [Required]
        public string Password { get; set; }
        public string? ShortInfo { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }

        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public virtual List<Week> Week { get; set; }
        public virtual List<AbonementsUser> AbonementsUser { get; set; }
        public virtual List<TrenersUser> TrenersUser { get; set; }
        public virtual List<StrangeUserRecord> StrangeUserRecord { get; set; }
        public User()
        {
            StrangeUserRecord = new List<StrangeUserRecord>();
            Week = new List<Week>();
            AbonementsUser = new List<AbonementsUser>();
            TrenersUser = new List<TrenersUser>();
        }

       
        
    }
}
