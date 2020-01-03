using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;
using BankBer.BackEnd.Data_Access;
using BankBer.BackEnd.Models;
using BankBer.BackEnd.Models.Account;

namespace BankBer.BackEnd.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        [HttpGet]
        public List<Account> GetAllAccounts()
        {
            var accountDao = new AccountDao();
            return accountDao.GetAllAccounts();
        }
        
        [HttpGet]
        public List<Account> GetAccountsForUser([FromUri] Guid userId)
        {
            var dao = new AccountDao();
            return dao.GetAccountsForUser(userId);
        }

        [HttpGet]
        [Route("{accountId:Guid}")]
        public Account GetSingleAccount(Guid accountId)
        {
            var accountDao = new AccountDao();
            return accountDao.GetAccountById(accountId);
        }

        [HttpPost]
        public Account NewAccount(NewAccount newAccount)
        {
            var accountDao = new AccountDao();
            return accountDao.InsertAccount(newAccount);
        }

        [HttpPut]
        [Route("{accountId:Guid}")]
        public void UpdateAccount(Guid accountId, UpdateAccount accountToUpdate)
        {
            var accountDao = new AccountDao();
            accountDao.UpdateAccount(accountId, accountToUpdate);
        }
    }
}
