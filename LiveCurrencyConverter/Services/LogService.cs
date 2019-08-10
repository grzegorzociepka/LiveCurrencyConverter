using LiveCurrencyConverter.DTO;
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
        public void AddLog(LogDTO logDto)
        {
            var logToInsert = new Log(logDto);
            _context.Logs.Add(logToInsert);
            _context.SaveChanges();
        }
    }
}