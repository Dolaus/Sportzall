using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using System.Collections.Generic;
using System.Data;

namespace Sportzall.Controllers
{
    public class AbonementController : Controller
    {
        private readonly SportzalDBContext _dbContext;

        public AbonementController(SportzalDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            IEnumerable<Abonement> abonements = _dbContext.Abonement;
           
            return View(abonements);
        }
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            var user = _dbContext.User.Include(u => u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login","Account");
            }
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var Buyer = _dbContext.User.FirstOrDefault(u => u.Email == User.Identity.Name);
            var abonement = _dbContext.Abonement.Find(id);


            AbonementsUser abonementNew = new AbonementsUser()
            {
                Name = abonement.Name,
                Price = abonement.Price,
                Description = abonement.Description,
                UserId = Buyer.Id,
                IsPay = false
            };

            _dbContext.AbonementsUser.Add(abonementNew);
            _dbContext.SaveChanges();
            if (abonement == null)
            {
                return NotFound();
            }


            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userAbonement = _dbContext.AbonementsUser.Find(id);
            if (userAbonement == null)
            {
                return NotFound();
            }
            _dbContext.AbonementsUser.Remove(userAbonement);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", "User", new { id = userAbonement.UserId });
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Abonement abonement)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Abonement.Add(abonement);
                _dbContext.SaveChanges();
            }
            return View();
        }


        [HttpGet]
        public IActionResult DeleteUserAbonement(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var abonement = _dbContext.AbonementsUser.Find(id);
            var currentuser = _dbContext.User.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (abonement == null)
            {
                return NotFound();
            }
            _dbContext.AbonementsUser.Remove(abonement);
            _dbContext.SaveChanges();
            return RedirectToAction("AboutUser", "User", new { id = currentuser.Id });
        }
       
    }
}
