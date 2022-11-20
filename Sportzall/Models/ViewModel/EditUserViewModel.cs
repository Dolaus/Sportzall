using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sportzall.Models.ViewModel
{
    public class EditUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> ?RoleSelectList { get; set; }
    }
}
