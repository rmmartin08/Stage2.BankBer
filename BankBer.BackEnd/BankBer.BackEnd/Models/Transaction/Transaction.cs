using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BankBer.BackEnd.Models.Transaction
{
    public class Transaction
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TransactionType
        {
            Debit,
            Credit
        }
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}