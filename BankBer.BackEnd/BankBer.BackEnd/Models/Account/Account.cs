using System;
using System.Collections.Generic;

namespace BankBer.BackEnd.Models.Account
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}