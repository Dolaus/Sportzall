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
            var UserTeam = _dbContext.User.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            User Trener = _dbContext.User.FirstOrDefault(u=>u.Email==User.Identity.Name);
            if (!_dbContext.TrenersUser.Any(u => u.UnicKey == UserTeam.Id))
            {
                TrenersUser trenersUser = new TrenersUser()
                {
                    UnicKey = UserTeam.Id,
                    Name = UserTeam.Name,
                    Year = UserTeam.Year,
                    UserId = Trener.Id
                };


                _dbContext.TrenersUser.Add(trenersUser);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(AllUser));
        }
        [HttpGet]
        public IActionResult MyTeam()
        {
            User Trener = _dbContext.User.FirstOrDefault(u => u.Email == User.Identity.Name);
            var team = _dbContext.TrenersUser.Where(u => u.UserId == Trener.Id);
            return View(team);
        }
        [HttpGet]
        public IActionResult DeleteFromTeam(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var user = _dbContext.TrenersUser.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.TrenersUser.Remove(user);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(MyTeam));
        }
    }
}
