using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankBer.BackEnd.Data_Access;
using BankBer.BackEnd.Models;
using BankBer.BackEnd.Models.Transaction;

namespace BankBer.BackEnd.Controllers
{
    [RoutePrefix("api/transactions")]
    public class TransactionsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTransactions(Guid accountId = default(Guid))
        {
            if (accountId == default(Guid))
            {
                return BadRequest();
            }

            var transactionDao = new TransactionDao();
            return Ok(transactionDao.GetAllTransactionsForAccount(accountId));
        }

        [HttpPost]
        public Transaction AddTransaction(NewTransaction newTransaction)
        {
            var transactionDao = new TransactionDao();
            return transactionDao.InsertTransaction(newTransaction);
        }
    }
}