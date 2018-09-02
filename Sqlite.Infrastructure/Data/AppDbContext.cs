using Microsoft.EntityFrameworkCore;
using Sqlite.Core.Entities;

namespace Sqlite.Infrastructure.Data
{
    /*
     * Issue: EF Core cannot drop or alter column using the migrations
     * WorkAround: Need to drop the table,remove migrations and re-apply them again
     * Reference:https://stackoverflow.com/questions/41902905/ef-core-dropping-a-column-workaround-sqlite
     */

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
            ExpenseTypeEntityModeling(modelBuilder);
        }

        private static void ExpenseTypeEntityModeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseType>()
                .Property(e => e.Type)
                .IsRequired();
            modelBuilder.Entity<ExpenseType>()
                .Property(e => e.AddedOn)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ExpenseType>()
                .Property(e => e.LastModifiedOn)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}