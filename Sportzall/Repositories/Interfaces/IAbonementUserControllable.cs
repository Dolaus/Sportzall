using Sportzall.Models;

namespace Sportzall.Repositories.Interfaces
{
    public interface IAbonementUserControllable
    {
        AbonementsUser FindAbonementById(int id);
        void DeleteAbonementUser(AbonementsUser abonementsUser);
        void AddAbonementUser(AbonementsUser abonementsUser);
    }
}
