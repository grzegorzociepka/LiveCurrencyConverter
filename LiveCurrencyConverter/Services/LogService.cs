using LiveCurrencyConverter.Entities;
using LiveCurrencyConverter.Repository;
using LiveCurrencyConverter.Services.Interfaces;

namespace LiveCurrencyConverter.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationContext _context;

        public LogService(ApplicationContext context)
        {
            _context = context;
        }

        public void AddLog(string desc)
        {
            var logToInsert = new Log(desc);
            _context.Logs.Add(logToInsert);
            _context.SaveChanges();
        }
    }
}