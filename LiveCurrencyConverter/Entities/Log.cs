using System;
using System.ComponentModel.DataAnnotations.Schema;
using LiveCurrencyConverter.DTO;

namespace LiveCurrencyConverter.Entities
{
    public class Log : IEntity
    {
        [Column("LogId")]
        public int Id { get; set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }

        public Log(LogDTO logDto)
        {
            Description = logDto.Description;
            Date = DateTime.Now;
        }

        public Log()
        {
            
        }
        public void setDescription(string desc)
        {
            Description = desc;
            
        }
    }
}