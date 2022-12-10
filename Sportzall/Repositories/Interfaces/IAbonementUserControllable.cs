using Sportzall.Models;

namespace Sportzall.Repositories.Interfaces
{
    public interface IAbonementUserControllable
    {
        void FindAbonementById(int id);
        void DeleteAbonementUser(AbonementsUser abonementsUser);
        void AddAbonementUser(AbonementsUser abonementsUser);
    }
}
