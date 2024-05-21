using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public SystemAccount GetAccountById(string accountID) => SystemAccountDAO.GetAccountById(accountID);
    }
}
