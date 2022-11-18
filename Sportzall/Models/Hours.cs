using Microsoft.Build.ObjectModelRemoting;

namespace Sportzall.Models
{
    public class Hours
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int WeekId { get; set; }
        public Week Week { get; set; }
        public bool IsBusy { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public int ?UserId { get; set; }
        
    }
}
