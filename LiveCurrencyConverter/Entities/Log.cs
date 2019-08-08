using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveCurrencyConverter.Entities
{
    public class Log : IEntity
    {
        [Column("LogId")]
        public int Id { get; set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }

        public Log()
        {
            Date = DateTime.Now;
        }

        public void setDescription(string desc)
        {
            Description = desc;
            
        }
    }
}