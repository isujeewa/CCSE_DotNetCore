using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CCSE.TransactionApi.Models;

namespace CCSE.TransactionApi.Data
{
    public class TransactionContext : DbContext
    {
        public TransactionContext (DbContextOptions<TransactionContext> options)
            : base(options)
        {
        }

        public DbSet<CCSE.TransactionApi.Models.Transaction> Transaction { get; set; } = default!;
    }
}
