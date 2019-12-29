using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankBer.BackEnd.Models;
using LiteDB;

namespace BankBer.BackEnd.Data_Access
{
    public class DbAccount
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Guid> TransactionIds { get; set; }
    }

    public class AccountDao: DaoBase
    {
        public Account InsertAccount(Account accountToInsert)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<DbAccount>("Accounts");

                var newAccount = new DbAccount()
                {
                    Id = Guid.NewGuid(),
                    Type = accountToInsert.Type,
                    UserId = accountToInsert.UserId,
                    Name = accountToInsert.Name
                };

                accountCol.Insert(newAccount);

                accountToInsert.Id = newAccount.Id;
                return accountToInsert;
            }
        }

        public Account GetAccountById(Guid id)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<DbAccount>("Accounts");

                var account = accountCol.FindById(id);
                if (account == null)
                {
                    throw new KeyNotFoundException();
                }

                var transactionDao = new TransactionDao();

                return ConvertDbAccountToAccount(account);
            }
        }

        public Account[] GetAccountsForUser(Guid userId)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<DbAccount>("Accounts");

                var foundAccounts = accountCol.Find(Query.EQ("UserId", userId));

                return foundAccounts.Select(ConvertDbAccountToAccount).ToArray();
            }
        }

        public Account[] GetAllAccounts()
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<DbAccount>("Accounts");

                var accounts = accountCol.FindAll().ToArray();

                return accounts.Select(ConvertDbAccountToAccount).ToArray();
            }
        }

        public void UpdateAccount(Account accountToUpdate)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var userCol = db.GetCollection<DbAccount>("Accounts");

                var foundAccount = userCol.FindById(accountToUpdate.Id.Value);

                if (foundAccount == null)
                {
                    throw new KeyNotFoundException();
                }

                if (!string.IsNullOrWhiteSpace(accountToUpdate.Type))
                {
                    foundAccount.Type = accountToUpdate.Type;
                }

                if (!string.IsNullOrWhiteSpace(accountToUpdate.Name))
                {
                    foundAccount.Name = accountToUpdate.Name;
                }

                return;
            }
        }

        private Account ConvertDbAccountToAccount(DbAccount account)
        {
            var transactionDao = new TransactionDao();
            return new Account()
            {
                Id = account.Id,
                Type = account.Type,
                UserId = account.UserId,
                Name = account.Name,
                Transactions =
                    account.TransactionIds?.Select(t => transactionDao.GetTransactionById(t)).ToArray() ??
                    new Transaction[0]
            };
        }
    }
}