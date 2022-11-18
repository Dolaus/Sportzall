using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using Sportzall.Models.ViewModel;

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
        public IActionResult Details(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var user = _dbContext.User.Include(u=>u.AbonementsUser).FirstOrDefault(u=>u.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            EditUserViewModel editUserViewModel = new EditUserViewModel()
            {
                User=new User(),
                RoleSelectList=_dbContext.Role.Select(u=> new SelectListItem
                {
                    Text =u.Name,
                    Value=u.Id.ToString()
                })
            };
            if (id==null||id==0)
            {
                return View(editUserViewModel);
            }

            editUserViewModel.User = _dbContext.User.Find(id);
            if (editUserViewModel == null)
            {
                return NotFound();
            }
            return View(editUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {

            if (ModelState.IsValid)
            {
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
        [HttpGet]
        public IActionResult AboutUser()
        {
            var user = _dbContext.User.Include(u=>u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login","Account");
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Bascket()
        {
            var user = _dbContext.User.Include(i => i.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user==null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Pay(int? id)
        {
            if (id==0||id==null)
            {
                return NotFound();
            }
            var abonementsUser = _dbContext.AbonementsUser.Find(id);
            if (abonementsUser == null)
            {
                return NotFound();
            }
            abonementsUser.IsPay = true;
            _dbContext.AbonementsUser.Update(abonementsUser);
            _dbContext.SaveChanges();
            return RedirectToAction("Bascket");
        }
        [HttpGet]
        public IActionResult AdminPanel()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TrenerPanel()
        {
            return View();
        }
        
    }
}
