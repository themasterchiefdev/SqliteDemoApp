using Microsoft.EntityFrameworkCore;
using Sqlite.Core.Entities;

namespace Sqlite.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=expensetracking.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}