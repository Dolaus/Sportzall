using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using Sportzall.Models.ViewModel;
using Sportzall.Repositories.Interfaces;

namespace Sportzall.Controllers
{
    public class UserController : Controller
    {
        private readonly SportzalDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IUserControllable _userControllable;

        public UserController(IWebHostEnvironment webHostEnviroment,SportzalDBContext sportzalDBContext,IUserControllable userControllable)
        {
            _webHostEnviroment = webHostEnviroment;
            _dbContext = sportzalDBContext;
            _userControllable = userControllable;
        }

        public IActionResult Index()
        {
            IEnumerable<User> users = _userControllable.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateUserViewModel model = new CreateUserViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel model)
        {
            var fileimages = HttpContext.Request.Form.Files;
            if (ModelState.IsValid)
            {
                 if (fileimages.Count > 0)
                {
                    var webpath = _webHostEnviroment.WebRootPath;
                    string upload = webpath + URL.ImageUserURL;
                    string imageName = Guid.NewGuid().ToString();
                    string imageextension = Path.GetExtension(fileimages[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                    {
                        fileimages[0].CopyTo(fileStream);
                    }
                    model.Image = imageName + imageextension;
                }
                User user= new User();
                user.Name = model.Name;
                user.Email = model.Email;
                user.Year = model.Year;
                user.Role = _dbContext.Role.FirstOrDefault(r => r.Name == "user");
                user.Id = model.Id;
                user.Password = model.Password;
                user.Image = model.Image;
                user.PhoneNumber = model.PhoneNumber;
                user.ShortInfo = model.ShortInfo;
                user.BenchPress = model.BenchPress;
                user.ChessPress = model.ChessPress;
                user.Squat = model.Squat;

                _userControllable.AddUser(user);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var user = _userControllable.FindUserById(id);
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

            editUserViewModel.User = _userControllable.FindUserById(id);
            if (editUserViewModel == null)
            {
                return NotFound();
            }
            return View(editUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel model)
        {
            User user = _userControllable.FindUserById(model.User.Id);

            if (ModelState.IsValid)
            {
                var fileimages = HttpContext.Request.Form.Files;
                if (fileimages.Count > 0)
                {
                    var webpath = _webHostEnviroment.WebRootPath;
                    string upload = webpath + URL.ImageUserURL;
                    string imageName = Guid.NewGuid().ToString();
                    string imageextension = Path.GetExtension(fileimages[0].FileName);

                    if (user.Image != null)
                    {
                        var oldFile = Path.Combine(upload, user.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                            using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                            {
                                fileimages[0].CopyTo(fileStream);
                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                        {
                            fileimages[0].CopyTo(fileStream);
                        }
                    }
                    user.Image = imageName + imageextension;
                }
                user.Year = model.User.Year;
                user.RoleId = model.User.RoleId;
                user.Email = model.User.Email;
                user.Password=model.User.Password;
                user.PhoneNumber = model.User.PhoneNumber;
                user.ShortInfo = model.User.ShortInfo;
                user.ChessPress = model.User.ChessPress;
                user.BenchPress = model.User.BenchPress;
                user.Squat = model.User.Squat;
                _userControllable.UpdateUser(user);
                return RedirectToAction("Index");
            }
            model.RoleSelectList = _dbContext.Role.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(model);
        }

        [HttpGet]
        public IActionResult AddSomeInformationAboutUser()
        {
            var user = _dbContext.User.Include(u => u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            return View(user);
        }
        [HttpPost]
        public IActionResult AddSomeInformationAboutUser(User user)
        {
            User olduser = _userControllable.FindUserById(user.Id);

            if (ModelState.IsValid)
            {
                var fileimages = HttpContext.Request.Form.Files;
                if (fileimages.Count > 0)
                {
                    var webpath = _webHostEnviroment.WebRootPath;
                    string upload = webpath + URL.ImageUserURL;
                    string imageName = Guid.NewGuid().ToString();
                    string imageextension = Path.GetExtension(fileimages[0].FileName);

                    if (olduser.Image != null)
                    {
                        var oldFile = Path.Combine(upload, olduser.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                            using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                            {
                                fileimages[0].CopyTo(fileStream);
                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                        {
                            fileimages[0].CopyTo(fileStream);
                        }
                    }
                    olduser.Image = imageName + imageextension;
                }
                olduser.Year = user.Year;
                olduser.RoleId = user.RoleId;
                olduser.Email = user.Email;
                olduser.Password = user.Password;
                olduser.Name = user.Name;
                olduser.PhoneNumber = user.PhoneNumber;
                olduser.ShortInfo = user.ShortInfo;
                olduser.ChessPress = user.ChessPress;
                olduser.BenchPress = user.BenchPress;
                olduser.Squat = user.Squat;
                _dbContext.User.Update(olduser);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(AboutUser));
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

            var webpath = _webHostEnviroment.WebRootPath;
            string upload = webpath + URL.ImageUserURL;
            if (user!=null)
            {
                if (user.Image != null)
                {
                    var oldFile = Path.Combine(upload, user.Image);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }
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
            var user = _dbContext.User.Include(u=>u.AbonementsUser).Include(i=>i.StrangeUserRecord).FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login","Account");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult MyRecords()
        {
            var user = _dbContext.User.Include(u => u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            var allYourrecord = _dbContext.Hours.Include(u => u.Week).ThenInclude(u => u.User).Where(u => u.UserId == user.Id);

            return View(allYourrecord);
        }
        [HttpGet]
        public IActionResult MyAbonements()
        {
            var user = _dbContext.User.Include(u => u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
           
            return View(user);
        }
        [HttpGet]
        public IActionResult MyTreners()
        {
            var user = _dbContext.User.Include(u => u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            var yourPersonalTrener = _dbContext.TrenersUser.Include(u => u.User).Where(u => u.UnicKey == user.Id);
            return View(yourPersonalTrener);
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
        [HttpGet]
        public IActionResult SelectDayAddMyToRozklad(int trenersid)
        {
            if (trenersid==null||trenersid==0)
            {
                return NotFound();
            }
            var user = _dbContext.User.Find(trenersid);
            if (user==null)
            {
                return NotFound();
            }
            var rozklad = _dbContext.Week.Include(u => u.User).Where(u => u.UserId == user.Id);
            if (rozklad == null)
            {
                return NotFound();
            }
            return View(rozklad);
        }
        [HttpGet]
        public IActionResult SelectHourOfDayAddMeToRozklad(int trenersid)
        {
            var currentuser = _dbContext.User.Include(u => u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            if (trenersid == null || trenersid == 0)
            {
                return NotFound();
            }
            var dayHours = _dbContext.Hours.Include(u => u.Week).Where(u => u.WeekId == trenersid);
            if (dayHours == null)
            {
                return NotFound();
            }
            SelectHourOfDayAddMeToRozkladViewModel selectHourOfDayAddMeToRozkladViewModel = new SelectHourOfDayAddMeToRozkladViewModel()
            {
                Hours = dayHours,
                user = currentuser
            };
            return View(selectHourOfDayAddMeToRozkladViewModel);
        }
        [HttpGet]
        public IActionResult AddMeToTrenersRozklad(int id)
        {
            var hour = _dbContext.Hours.Find(id);
            if (hour == null)
            {
                return NotFound();
            }
            var user = _dbContext.User.Include(u => u.AbonementsUser).FirstOrDefault(u => u.Email == User.Identity.Name);
            hour.UserId = user.Id;
            _dbContext.Hours.Update(hour);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(SelectHourOfDayAddMeToRozklad), new {trenersid=hour.WeekId});
        }
        [HttpGet]
        public IActionResult DeleteMeToTrenersRozklad(int id)//Винести логіку з DeleteMeToTrenersRozklad
        {
            var hour = _dbContext.Hours.Find(id);
            if (hour==null)
            {
                return NotFound();
            }
            hour.UserId = null;
            hour.IsBusy = false;
            _dbContext.Hours.Update(hour);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(SelectHourOfDayAddMeToRozklad), new { trenersid = hour.WeekId });
        }

        [HttpGet]//Винести логіку з DeleteMeToTrenersRozklad
        public IActionResult DeleteMeTofromMyRecords(int id)
        {
            var hour = _dbContext.Hours.Find(id);
            if (hour == null)
            {
                return NotFound();
            }
            hour.UserId = null;
            hour.IsBusy = false;
            _dbContext.Hours.Update(hour);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(MyRecords));
        }
    }
}
