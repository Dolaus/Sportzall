namespace Sportzall.Models
{
    public class AbonementsUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsPay { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        
    }
}
