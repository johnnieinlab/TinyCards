using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyCards.Core.Model;

namespace TinyCards.Core.Data
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(
            DbContextOptions<CardDbContext> options) : base(options)  { }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .ToTable("Card");

        }
    }
}
