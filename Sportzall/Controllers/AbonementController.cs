﻿using Microsoft.AspNetCore.Mvc;
using Sportzall.Models;

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
            IEnumerable < Abonement > abonements = _dbContext.Abonement;
            
            return View(abonements);
        }

        public IActionResult Buy(int? id)
        {
            if (id == null||id==0)
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
                IsPay=false
            };
          
            _dbContext.AbonementsUser.Add(abonementNew);
            _dbContext.SaveChanges();
            if (abonement==null)
            {
                return NotFound();
            }


            return RedirectToAction( "Index");
        }
    }
}
