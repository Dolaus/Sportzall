namespace Sportzall.Models
{
    public class Week
    {
        public int Id { get; set; }
        public string NameofDay { get; set; }


        //один до багатьох
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual List<Hours>? Hours { get; set; }
        public Week()
        {
            Hours = new List<Hours>();
        }
    }
}
