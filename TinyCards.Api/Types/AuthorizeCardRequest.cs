using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCards.Core.Constant;

namespace TinyCards.Api.Types
{
    public class AuthorizeCardRequest
    {
        public string CardNumber { get; set; }
        public decimal amount { get; set; }
        public TransactionType type { get; set; }
        public string description { get; set; }
    }
}
