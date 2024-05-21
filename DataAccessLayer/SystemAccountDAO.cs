using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
