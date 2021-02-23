using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCards.Core.Constant;
using TinyCards.Core.Data;
using TinyCards.Core.Interface;
using TinyCards.Core.Model;

namespace TinyCards.Core.Service
{
    class CardService : ICardService
    {
        private CardDbContext _dbContext;
        public CardService(CardDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<Transaction>> AuthorizeAsync(string cardNumber, decimal amount, TransactionType type, string details)
        {
            try
            {
                if (type == TransactionType.Undefined)
                {
                    return new Result<Transaction> { Code = ResultCode.Error, ErrorMessage = "Transaction type not provided" };
                }

                var card = await GetByNumberAsync(cardNumber);
                if (card == null)
                {
                    return new Result<Transaction> { Code = ResultCode.NotFound, ErrorMessage = "Card not found" }; // we shouldn't actually return "notfound" as an outer message. that could be abused.
                }

                if (amount > card.Balance)
                {
                    return new Result<Transaction> { Code = ResultCode.Error, ErrorMessage = "Not available balance" };
                }

                //check daily limits
                
                var limit = await GetCardLimitAsync(cardNumber, type);
                var today = DateTime.Today.ToString("yyyy-MM-dd"); //datetime format should be global. I use this to avoid any confussion with datetime formats. standard ISO date.

                //no null check, limits will be init on card creation
                //limit = new CardLimit { AggregateAmount = amount, Card = card, IsoDate = today, LimitType = type };


                if (today == limit.IsoDate && (limit.AggregateAmount + amount) > HardLimits.GetLimit(type))
                {
                    return new Result<Transaction> { Code = ResultCode.Error, ErrorMessage = "Limit reached" };
                }


                if (today == limit.IsoDate)
                {
                    limit.AggregateAmount += amount;
                }
                else
                {
                    limit.IsoDate = today;
                    limit.AggregateAmount = amount;
                }

                card.Balance -= amount;

                var transaction = new Transaction { Card = card, Id = Guid.NewGuid(), Amount = amount, Type = type, Timestamp = DateTimeOffset.Now, Details = details };
                await _dbContext.AddAsync(transaction);

                // the following will persist transaction, limit and card details
                //
                //
                await _dbContext.SaveChangesAsync();
                return new Result<Transaction> { Data = transaction, Code = ResultCode.Success };
            } 
            catch (Exception e)
            {
                return new Result<Transaction> { Code = ResultCode.Error, ErrorMessage = e.Message };
            }

        }

        public async Task<Card> ChargeAsync(string cardNumber, decimal amount)
        {
            var card = await _dbContext.Set<Card>()
                .Where(c => c.Number == cardNumber)
                .SingleOrDefaultAsync();

            card.Balance += amount;
            await _dbContext.SaveChangesAsync();
            return card; 
        }

        public async Task<Card> GetByNumberAsync(string cardNumber)
        {
            return await _dbContext.Set<Card>()
                .Where(c => c.Number == cardNumber)
                .SingleOrDefaultAsync();
        }

        public async Task<CardLimit> GetCardLimitAsync(string cardNumber, TransactionType type)
        {
            return await _dbContext.Set<CardLimit>()
                .Where(c => c.CardNumber == cardNumber)
                .Where(c => c.LimitType == type)
                .SingleOrDefaultAsync();
        }

        public async Task<CardLimit> CreateLimitAsync(string cardNumber, decimal amount, TransactionType type)
        {
            var limit = new CardLimit { AggregateAmount = amount, LimitType = type, CardNumber = (await GetByNumberAsync(cardNumber)).Number };
            await _dbContext.AddAsync(limit);
            return limit;
        }

        public async Task<Result<Card>> RegisterAsync(string cardNumber, decimal amount)
        {
            try
            {
                var card = new Card { Id = Guid.NewGuid(), Number = cardNumber, Balance = amount };
                var today = DateTime.Today.ToString("yyyy-MM-dd");
                var cardLimits = new List<CardLimit>
                {
                    new CardLimit
                    {
                        CardNumber = cardNumber,
                        IsoDate = today,
                        AggregateAmount = 0,
                        LimitType = TransactionType.CardPresent
                    },
                    new CardLimit
                    {
                        CardNumber = cardNumber,
                        IsoDate = today,
                        AggregateAmount = 0,
                        LimitType = TransactionType.ECommerse
                    }
                };
                _dbContext.Add(card);
                _dbContext.AddRange(cardLimits);
                await _dbContext.SaveChangesAsync();
                return new Result<Card> { Code = ResultCode.Success, Data = card };
            }
            catch (Exception e)
            {
                return new Result<Card> { Code = ResultCode.Error, ErrorMessage = $"Create card failed [{e.Message}]" };
            }

        }
    }
}
