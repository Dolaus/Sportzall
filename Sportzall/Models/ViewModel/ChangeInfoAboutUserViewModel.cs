using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sportzall.Models.ViewModel
{
    public class ChangeInfoAboutUserViewModel
    {
        public int UserId { get; set; }
        public User user { get; set; }
        public Role UserRole { get; set; }
        public List<Role> AllRoles { get; set; }
        public IEnumerable<SelectListItem> RolecSelectListItem { get; set; }
        public ChangeInfoAboutUserViewModel()
        {
            AllRoles = new List<Role>();
        }
    }
}
