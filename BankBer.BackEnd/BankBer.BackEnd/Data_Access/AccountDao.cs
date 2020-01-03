using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankBer.BackEnd.Models;
using BankBer.BackEnd.Models.Account;
using LiteDB;

namespace BankBer.BackEnd.Data_Access
{
    public class AccountDao : DaoBase
    {
        public Account InsertAccount(NewAccount accountToInsert)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<Account>("Accounts");

                var account = new Account()
                {
                    Id = Guid.NewGuid(),
                    Name = accountToInsert.Name,
                    Type = accountToInsert.Type,
                    UserId = accountToInsert.UserId
                };

                accountCol.Insert(account);

                return account;
            }
        }

        public Account GetAccountById(Guid id)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<Account>("Accounts");

                var account = accountCol.FindById(id);
                if (account == null)
                {
                    throw new KeyNotFoundException();
                }

                var transactionDao = new TransactionDao();

                return account;
            }
        }

        public List<Account> GetAccountsForUser(Guid userId)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<Account>("Accounts");

                var foundAccounts = accountCol.Find(Query.EQ("UserId", userId));

                return foundAccounts.ToList();
            }
        }

        public List<Account> GetAllAccounts()
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var accountCol = db.GetCollection<Account>("Accounts");

                var accounts = accountCol.FindAll();

                return accounts.ToList();
            }
        }

        public void UpdateAccount(Guid accountId, UpdateAccount accountToUpdate)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var userCol = db.GetCollection<Account>("Accounts");

                var foundAccount = userCol.FindById(accountId);

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
    }
}