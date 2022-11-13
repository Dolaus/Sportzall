namespace Sportzall.Models
{
    public class TrenersUser
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int? Year { get; set; }
        public int UnicKey { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
