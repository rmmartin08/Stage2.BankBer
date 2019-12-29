using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankBer.BackEnd.Models;
using LiteDB;

namespace BankBer.BackEnd.Data_Access
{
    public class UserDao : DaoBase
    {
        public User InsertUser(User userToInsert)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var userCol = db.GetCollection<User>("Users");

                if (!userToInsert.Id.HasValue)
                {
                    userToInsert.Id = Guid.NewGuid();
                }

                userCol.Insert(userToInsert);
                return userToInsert;
            }
        }

        public User GetUserById(Guid id)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var userCol = db.GetCollection<User>("Users");

                var user = userCol.FindById(id);
                return user;
            }
        }

        public User[] GetAllUsers()
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var userCol = db.GetCollection<User>("Users");

                var users = userCol.FindAll().ToArray();
                return users;
            }
        }

        public void UpdateUser(User userToUpdate)
        {
            using (var db = new LiteDatabase(BankBerDbLocation))
            {
                var userCol = db.GetCollection<User>("Users");

                var foundUser = userCol.FindById(userToUpdate.Id.Value);

                if (foundUser == null)
                {
                    throw new KeyNotFoundException();
                }

                if (!string.IsNullOrWhiteSpace(userToUpdate.FirstName))
                {
                    foundUser.FirstName = userToUpdate.FirstName;
                }

                if (!string.IsNullOrWhiteSpace(userToUpdate.LastName))
                {
                    foundUser.LastName = userToUpdate.LastName;
                }

                return;
            }
        }
    }
}