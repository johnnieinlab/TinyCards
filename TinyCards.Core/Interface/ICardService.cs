using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCards.Core.Model;
using TinyCards.Core.Constant;

namespace TinyCards.Core.Interface
{
    interface ICardService
    {
        public Task<Result<Transaction>> AuthorizeAsync(string cardNumber, decimal amount, TransactionType type, string details);
        public Task<Card> GetByNumberAsync(string cardNumber);
        public Task<Card> ChargeAsync(string cardNumber, decimal amount);

        public Task<CardLimit> GetCardLimitAsync(string cardNumber, TransactionType type);
    }
}
