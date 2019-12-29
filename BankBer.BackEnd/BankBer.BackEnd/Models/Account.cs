using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankBer.BackEnd.Models
{
    public class Account
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Transaction[] Transactions { get; set; }
        public string Type { get; set; }
    }
}