using Sportzall.Models;
using Sportzall.Repositories.Interfaces;

namespace Sportzall.Repositories.Implementations
{
    public class AbonementUserControllable : IAbonementUserControllable
    {
        private readonly SportzalDBContext _dbContext;
        public AbonementUserControllable(SportzalDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAbonementUser(AbonementsUser abonementsUser)
        {
            _dbContext.Add(abonementsUser);
            _dbContext.SaveChanges();
        }

        public void DeleteAbonementUser(AbonementsUser abonementsUser)
        {
            _dbContext.Remove(abonementsUser);
            _dbContext.SaveChanges();
        }

        public AbonementsUser FindAbonementById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AbonementsUser> GetAllAbonements()
        {
            throw new NotImplementedException();
        }
    }
}
