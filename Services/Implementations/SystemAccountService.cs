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

        public SystemAccount GetAccountById(string accountID)
        {
            return iAccountRepository.GetAccountById(accountID);
        }
    }
}
