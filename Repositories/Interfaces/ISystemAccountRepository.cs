using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface ISystemAccountRepository
    {
        SystemAccount GetAccountById(string accountID);
    }
}
