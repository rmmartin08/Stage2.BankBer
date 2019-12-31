using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankBer.BackEnd.Data_Access;
using BankBer.BackEnd.Models;
using BankBer.BackEnd.Models.Account;

namespace BankBer.BackEnd.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        [HttpGet]
        public User[] GetAllUsers()
        {
            var dao = new UserDao();
            return dao.GetAllUsers();
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        public User GetSingleUser([FromUri] Guid userId)
        {
            var dao = new UserDao();
            return dao.GetUserById(userId);
        }

        [HttpPost]
        public User NewUser(User newUser)
        {
            var dao = new UserDao();
            return dao.InsertUser(newUser);
        }

        [HttpPut]
        public void UpdateUser(User updatingUser)
        {
            var dao = new UserDao();
            dao.UpdateUser(updatingUser);
        }
    }
}