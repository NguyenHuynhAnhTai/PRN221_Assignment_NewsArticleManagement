using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface ISystemAccountService
    {
        SystemAccount GetAccountById(string accountID);
    }
}
