using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankBer.BackEnd.Models.Transaction
{
    public class NewTransaction
    {
        [Required]
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Transaction.TransactionType Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}