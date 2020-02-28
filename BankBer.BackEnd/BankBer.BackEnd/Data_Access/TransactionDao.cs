using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BankBer.BackEnd.Models;
using BankBer.BackEnd.Models.Account;
using BankBer.BackEnd.Models.Transaction;
using LiteDB;

namespace BankBer.BackEnd.Data_Access
{
    public class TransactionDao: DaoBase
    {
        public List<Transaction> GetAllTransactionsForAccount(Guid accountId)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var transactionCol = db.GetCollection<Transaction>("Transactions");
                //var accountCol = db.GetCollection<Account>("Accounts");

                //var foundAccount = accountCol.FindById(accountId);
                //if (foundAccount == null)
                //{
                //    throw new KeyNotFoundException();
                //}

                var transactions = transactionCol.Find(t => t.AccountId == accountId);

                return transactions.ToList();
            }
        }

        public Transaction GetTransactionById(Guid transactionId)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var transactionCol = db.GetCollection<Transaction>("Transactions");

                return transactionCol.FindById(transactionId);
            }
        }

        public Transaction InsertTransaction(NewTransaction newTransaction)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountsCol = db.GetCollection<Account>("Accounts");
                var foundAccount = accountsCol.FindById(newTransaction.AccountId);
                if (foundAccount == null)
                {
                    throw new KeyNotFoundException();
                }

                var transactionCol = db.GetCollection<Transaction>("Transactions");

                var transaction = new Transaction()
                {
                    Id = Guid.NewGuid(),
                    AccountId = newTransaction.AccountId,
                    Timestamp = newTransaction.Timestamp,
                    Amount = newTransaction.Amount,
                    Description = newTransaction.Description,
                    Type = newTransaction.Type
                };

                transactionCol.Insert(transaction);

                return transaction;
            }
        }
    }
}