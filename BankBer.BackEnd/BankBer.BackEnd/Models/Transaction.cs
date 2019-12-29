using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BankBer.BackEnd.Models
{
    public class Transaction
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TransactionType
        {
            Debit,
            Credit
        }
        public Guid? Id { get; set; }
        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}