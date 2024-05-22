using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class SystemAccountDAO
    {
        public static SystemAccount GetAccountById(short accountID)
        {
            using var db = new FunewsManagementDbContext();
            return db.SystemAccounts.FirstOrDefault(x => x.AccountId.Equals(accountID));
        }

        public static SystemAccount? GetAccountByEmailAndPass(string email, string password)
        {
            using var db = new FunewsManagementDbContext();
            return db.SystemAccounts.FirstOrDefault(x => x.AccountEmail.Equals(email) && x.AccountPassword.Equals(password));
        }

        public static void UpdateAccount(SystemAccount p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Entry<SystemAccount>(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
