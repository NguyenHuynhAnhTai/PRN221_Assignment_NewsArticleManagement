using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface ISystemAccountService
    {
        void UpdateAccount(SystemAccount account);
        SystemAccount GetAccountById(short accountID);
        SystemAccount? GetAccountByEmailAndPass(string email, string password);
    }
}
