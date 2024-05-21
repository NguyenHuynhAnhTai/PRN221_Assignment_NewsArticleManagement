using BusinessObjects.Entities;

namespace DataAccessLayer
{
    public class SystemAccountDAO
    {
        public static SystemAccount GetAccountById(string accountID)
        {
            using var db = new FunewsManagementDbContext();
            return db.SystemAccounts.FirstOrDefault(x => x.AccountId.Equals(accountID));
        }
    }
}
