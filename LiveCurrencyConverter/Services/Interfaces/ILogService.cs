using LiveCurrencyConverter.DTO;

namespace LiveCurrencyConverter.Services.Interfaces
{
    public interface ILogService
    {
        void AddLog(LogDTO logDto);
    }
}