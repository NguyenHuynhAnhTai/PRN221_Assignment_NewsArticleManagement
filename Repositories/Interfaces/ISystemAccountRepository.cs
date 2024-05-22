using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface ISystemAccountRepository
    {
        void UpdateAccount(SystemAccount account);
        SystemAccount GetAccountById(short accountID);
        SystemAccount? GetAccountByEmailAndPass(string email, string password);
    }
}
