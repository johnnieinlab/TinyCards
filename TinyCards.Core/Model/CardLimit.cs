using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCards.Core.Constant;

namespace TinyCards.Core.Model
{
    class CardLimit
    {
        public Card Card { get; set; }
        public string IsoDate { get; set; }
        public TransactionType LimitType { get; set; }
        public decimal AggregateAmount { get; set; }
    }
}
