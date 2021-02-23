using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCards.Core.Constant;

namespace TinyCards.Core.Model
{
    class Transaction
    {
        public Guid Id { get; set; }
        public Card Card { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Details { get; set; } //e.g. Merchant, reason etc. Should be another Class.
    }
}
