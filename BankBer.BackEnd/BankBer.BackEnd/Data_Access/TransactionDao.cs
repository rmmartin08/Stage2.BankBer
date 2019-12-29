using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BankBer.BackEnd.Models;
using LiteDB;

namespace BankBer.BackEnd.Data_Access
{
    public class TransactionDao: DaoBase
    {
        public Transaction GetTransactionById(Guid transactionId)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var transactionCol = db.GetCollection<Transaction>("Transactions");

                return transactionCol.FindById(transactionId);
            }
        }

        public Transaction InsertTransaction(Guid accountId, Transaction newTransaction)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountsCol = db.GetCollection<DbAccount>("Accounts");
                var foundAccount = accountsCol.FindById(accountId);
                if (foundAccount == null)
                {
                    throw new KeyNotFoundException();
                }

                var transactionCol = db.GetCollection<Transaction>("Transactions");

                newTransaction.Id = Guid.NewGuid();
                newTransaction.Timestamp = DateTime.Now;

                transactionCol.Insert(newTransaction);

                if (foundAccount.TransactionIds == null)
                {
                    foundAccount.TransactionIds = new List<Guid>();
                }
                foundAccount.TransactionIds.Add(newTransaction.Id.Value);
                accountsCol.Update(foundAccount);

                return newTransaction;
            }
        }
    }
}