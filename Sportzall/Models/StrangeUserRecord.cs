namespace Sportzall.Models
{
    public class StrangeUserRecord
    {
        public int Id { get; set; }
        public int? ChessPress { get; set; }
        public int? BenchPress { get; set; }
        public int? Squat { get; set; }
        public string DateTimeDateTime { get; set; }
        public int UserId { get; set; }
     
        public virtual User User { get; set; }
    }
}
