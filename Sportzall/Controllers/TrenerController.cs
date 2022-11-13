using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;

namespace Sportzall.Controllers
{
    public class TrenerController:Controller
    {
        private readonly SportzalDBContext _dbContext;
        public TrenerController(SportzalDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult AllUser()
        {
            var user = _dbContext.User.Include(i => i.Role).Where(u => u.Role.Name == "user");

            return View(user);
        }
        [HttpGet]
        public IActionResult AddUserToTeam(int? id)
        {
            if (id == null||id==0)
            {
                return NotFound();
            }
            var User = _dbContext.User.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            TrenersUser trenersUser = new TrenersUser() { 
            UnicKey=User.Id,
            Name=User.Name,
            Year=User.Year,
            UserId=User.Id
            };

            _dbContext.TrenersUser.Add(trenersUser);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(AllUser));
        }
    }
}
