using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using System.Collections.Generic;

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
        [HttpGet]
        public IActionResult MyRozklad()
        {
            //List<Hours> hours= new List<Hours>() { new Hours { Name="9-10"},new Hours { Name="10-11"} };
            
            var user = _dbContext.User.FirstOrDefault(u => u.Email == User.Identity.Name);
            var count = _dbContext.Week.Where(u => u.UserId == user.Id).Count();

            if (count == null || count < 7)
            {
                Week monday = new Week()
                {
                    NameofDay = "Monday",
                    UserId = user.Id
                };
                Week tuesday = new Week()
                {
                    NameofDay = "Tuesday",
                    UserId = user.Id
                };
                Week wednesday = new Week()
                {
                    NameofDay = "Wednesday",
                    UserId = user.Id
                };
                Week thursday = new Week()
                {
                    NameofDay = "Thursday",
                    UserId = user.Id
                };
                Week friday = new Week()
                {
                    NameofDay = "Friday",
                    UserId = user.Id
                };
                Week saturday = new Week()
                {
                    NameofDay = "Saturday",
                    UserId = user.Id,

                };
                Week sunday = new Week()
                {
                    NameofDay = "Sunday",
                    UserId = user.Id
                };
                _dbContext.Week.Add(monday);
                _dbContext.Week.Add(tuesday);
                _dbContext.Week.Add(wednesday);
                _dbContext.Week.Add(thursday);
                _dbContext.Week.Add(friday);
                _dbContext.Week.Add(saturday);
                _dbContext.Week.Add(sunday);
                _dbContext.SaveChanges();
                Hours hours1 = new Hours()
                {
                    Name = "9-10",
                    WeekId=monday.Id
                };
                Hours hours2 = new Hours()
                {
                    Name = "10-11",
                    WeekId = monday.Id
                };
                _dbContext.Hours.Add(hours1);
                _dbContext.Hours.Add(hours2);
                _dbContext.SaveChanges();
            }

            var rozklad = _dbContext.Week.Include(u => u.User).Where(u => u.UserId == user.Id);
            if (rozklad==null)
            {
                return NotFound();
            }
            return View(rozklad);
        }
    }
}
