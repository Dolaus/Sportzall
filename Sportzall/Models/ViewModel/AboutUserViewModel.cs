namespace Sportzall.Models.ViewModel
{
    public class AboutUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<TrenersUser> Treners { get; set; }
        public IEnumerable<Hours> AllRecord { get; set; }
    }
}
