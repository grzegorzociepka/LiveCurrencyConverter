using LiveCurrencyConverter.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiveCurrencyConverter.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}