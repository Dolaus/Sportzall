using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using Sportzall.Repositories.Interfaces;

namespace Sportzall.Repositories.Implementations
{
    public class UserControllable : IUserControllable
    {
        private readonly SportzalDBContext _dbContext;
        public UserControllable(SportzalDBContext sportzalDBContext) { 
            _dbContext= sportzalDBContext;
        }
        public void AddUser(User user)
        {
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Hours> AllUserRecords(User user)
        {
            return _dbContext.Hours.Include(u => u.Week).ThenInclude(u => u.User).Where(u => u.UserId == user.Id);
        }

        public User FindUserByEmail(string? email)
        {
            return _dbContext.User.Include(u => u.AbonementsUser).Include(i => i.StrangeUserRecord).FirstOrDefault(u => u.Email == email);
        }

        public User FindUserById(int? id)
        {
            return _dbContext.User.Include(u => u.AbonementsUser).Include(u => u.StrangeUserRecord).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
          return _dbContext.User.Include(u => u.Role);
        }

        public void RemoveUser(User user)
        {
            _dbContext.User.Remove(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }
    }
}
