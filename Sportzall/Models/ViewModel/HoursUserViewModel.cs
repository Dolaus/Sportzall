namespace Sportzall.Models.ViewModel
{
    public class HoursUserViewModel
    {
        public int HoursId { get; set; }
        public IEnumerable<TrenersUser> Treners { get; set; }
    }
}
