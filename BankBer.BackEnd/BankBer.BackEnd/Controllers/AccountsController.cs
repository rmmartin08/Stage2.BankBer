using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;
using BankBer.BackEnd.Data_Access;
using BankBer.BackEnd.Models;

namespace BankBer.BackEnd.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        [HttpGet]
        public Account[] GetAllAccounts()
        {
            var accountDao = new AccountDao();
            return accountDao.GetAllAccounts();
        }

        [HttpGet]
        [Route("{accountId:Guid}")]
        public Account GetSingleAccount(Guid accountId)
        {
            var accountDao = new AccountDao();
            return accountDao.GetAccountById(accountId);
        }

        [HttpPost]
        public Account NewAccount(Account newAccount)
        {
            var accountDao = new AccountDao();
            return accountDao.InsertAccount(newAccount);
        }

        [HttpPut]
        public void UpdateAccount(Account accountToUpdate)
        {
            var accountDao = new AccountDao();
            accountDao.UpdateAccount(accountToUpdate);
        }

        [HttpPost]
        [Route("{accountId:Guid}/transactions")]
        public Transaction AddTransaction(Guid accountId, Transaction newTransaction)
        {
            var transactionDao = new TransactionDao();
            return transactionDao.InsertTransaction(accountId, newTransaction);
        }
    }
}
