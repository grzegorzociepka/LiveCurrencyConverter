using LiveCurrencyConverter.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiveCurrencyConverter.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./blog.db");
        }
    }
}