using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository iAccountRepository;

        public SystemAccountService(ISystemAccountRepository accountRepository)
        {
            iAccountRepository = accountRepository;
        }

        public SystemAccount? GetAccountByEmailAndPass(string email, string password)
        {
            return iAccountRepository.GetAccountByEmailAndPass(email, password);
        }

        public SystemAccount GetAccountById(short accountID)
        {
            return iAccountRepository.GetAccountById(accountID);
        }

        public void UpdateAccount(SystemAccount account)
        {
            iAccountRepository.UpdateAccount(account);
        }
    }
}
