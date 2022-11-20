using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using Sportzall.Models.ViewModel;

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
            if (!_dbContext.TrenersUser.Where(u=>u.UserId==Trener.Id).Any(u => u.UnicKey == UserTeam.Id))
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

                var days = _dbContext.Week.Where(u => u.UserId == user.Id);
                foreach (var item in days)
                {
                    Hours hours1 = new Hours()
                    {
                        Name = "9-10",
                        WeekId = item.Id
                    };
                    Hours hours2 = new Hours()
                    {
                        Name = "10-11",
                        WeekId = item.Id
                    };
                    Hours hours3 = new Hours()
                    {
                        Name = "11-12",
                        WeekId = item.Id
                    };
                    Hours hours4 = new Hours()
                    {
                        Name = "12-13",
                        WeekId = item.Id
                    };
                    Hours hours5 = new Hours()
                    {
                        Name = "13-14",
                        WeekId = item.Id
                    };
                    Hours hours6 = new Hours()
                    {
                        Name = "14-15",
                        WeekId = item.Id
                    };
                    Hours hours7 = new Hours()
                    {
                        Name = "15-16",
                        WeekId = item.Id
                    };
                    Hours hours8 = new Hours()
                    {
                        Name = "16-17",
                        WeekId = item.Id
                    };
                    Hours hours9 = new Hours()
                    {
                        Name = "17-18",
                        WeekId = item.Id
                    };

                    _dbContext.Hours.Add(hours1);
                    _dbContext.Hours.Add(hours2);
                    _dbContext.Hours.Add(hours3);
                    _dbContext.Hours.Add(hours4);
                    _dbContext.Hours.Add(hours5);
                    _dbContext.Hours.Add(hours6);
                    _dbContext.Hours.Add(hours7);
                    _dbContext.Hours.Add(hours8);
                    _dbContext.Hours.Add(hours9);
                }


                _dbContext.SaveChanges();
            }

            var rozklad = _dbContext.Week.Include(u => u.User).Where(u => u.UserId == user.Id);
            if (rozklad == null)
            {
                return NotFound();
            }
            return View(rozklad);
        }
        [HttpGet]
        public IActionResult MyDayHours(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var dayHours = _dbContext.Hours.Include(u => u.Week).Where(u=>u.WeekId==id);
            if (dayHours == null)
            {
                return NotFound();
            }
            return View(dayHours);
        }
        [HttpGet]
        public IActionResult MyUserHours(int id)
        {
            User Trener = _dbContext.User.FirstOrDefault(u => u.Email == User.Identity.Name);
            var team = _dbContext.TrenersUser.Where(u => u.UserId == Trener.Id);
            
            HoursUserViewModel hoursUserViewModel = new HoursUserViewModel()
            {
                HoursId = id,
                Treners= team
            };
            return View(hoursUserViewModel);
        }
        [HttpGet]
        public IActionResult DeleteUserHours(int id)
        {
            Hours hours = _dbContext.Hours.Find(id);
            if (hours == null)
            {
                return NotFound();
            }
            hours.IsBusy = false;
            hours.UserId = null;
            _dbContext.Hours.Update(hours);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(MyDayHours), new {id=hours.WeekId});
        }
        [HttpGet]
        public IActionResult ChangeStatus(int id)
        {
            Hours hours = _dbContext.Hours.Find(id);
            if (hours == null)
            {
                return NotFound();
            }
            if (hours.IsBusy == false)
            {
                hours.IsBusy = true;
            }
            else
            {
                hours.IsBusy = false;
            }
            _dbContext.Hours.Update(hours);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(MyDayHours), new { id = hours.WeekId });
        }
        
        [HttpGet]
        public IActionResult AddToTheTrenerAtHour(int id,int HoursId)
        {
            if (id==null||HoursId==null)
            {
                return NotFound();
            }
            var CurrentHourse = _dbContext.Hours.Find(HoursId);
            var user = _dbContext.TrenersUser.Find(id);
            if(user == null)
            {
                return NotFound();
            }
            if (CurrentHourse==null)
            {
                return NotFound();
            }
            CurrentHourse.IsBusy = true;
            CurrentHourse.UserId = user.UnicKey;
            _dbContext.Hours.Update(CurrentHourse);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(MyDayHours),new { id= CurrentHourse.WeekId});
        }
    }
}
