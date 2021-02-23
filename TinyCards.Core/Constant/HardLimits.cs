using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCards.Core.Constant
{
    public static class HardLimits
    {
        public static decimal GetLimit(TransactionType type)
        {
            if (type == TransactionType.CardPresent)
            {
                return 1500M;
            }
            if (type == TransactionType.ECommerse)
            {
                return 500M;
            }
            return 0;
        }
    }
}
