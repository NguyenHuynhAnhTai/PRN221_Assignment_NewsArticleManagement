using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountService iAccountService;

        public SystemAccountService(ISystemAccountService accountService)
        {
            iAccountService = accountService;
        }

        public SystemAccount GetAccountById(string accountID)
        {
            return iAccountService.GetAccountById(accountID);
        }
    }
}
