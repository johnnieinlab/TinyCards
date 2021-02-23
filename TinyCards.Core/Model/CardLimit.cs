using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCards.Core.Constant;

namespace TinyCards.Core.Model
{
    public class CardLimit
    {
        public string CardNumber { get; set; }
        public string IsoDate { get; set; }
        public TransactionType LimitType { get; set; }
        public decimal AggregateAmount { get; set; }
    }
}
