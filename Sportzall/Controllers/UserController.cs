using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;

namespace Sportzall.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SportzalDBContext _dbContext;

        public UserController(SportzalDBContext sportzalDBContext)
        {
            _dbContext = sportzalDBContext;
        }

        public IActionResult Index()
        {
            IEnumerable<User> users = _dbContext.User.Include(u => u.Role);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = _dbContext.Role.FirstOrDefault(r => r.Name == "user");
                _dbContext.User.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var user = _dbContext.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = _dbContext.Role.FirstOrDefault(r => r.Name == "user");
                _dbContext.User.Update(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var user = _dbContext.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var user = _dbContext.User.Find(id);
            if (user!=null)
            {          
                user.Role = _dbContext.Role.FirstOrDefault(r => r.Name == "user");
                _dbContext.User.Remove(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
