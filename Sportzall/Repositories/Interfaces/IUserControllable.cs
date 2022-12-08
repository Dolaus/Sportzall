using Sportzall.Models;

namespace Sportzall.Repositories.Interfaces
{
    public interface IUserControllable
    {
        
        User FindUserById(int? id);
        User FindUserByEmail(string? email);
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Hours> AllUserRecords(User user);
    }
}
