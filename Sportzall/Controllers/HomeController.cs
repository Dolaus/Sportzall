using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using System.Diagnostics;

namespace Sportzall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SportzalDBContext _dbContext;

        public HomeController(SportzalDBContext sportzalDBContext)
        {
            _dbContext=sportzalDBContext;
        }

        public IActionResult Index()
        {
            IEnumerable<User> users=_dbContext.User.Include(u=>u.Role).Where(u=>u.RoleId==3);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Create()
        //{

        //}
    }
}