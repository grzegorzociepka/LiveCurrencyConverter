using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveCurrencyConverter.Entities
{
    public class Log : IEntity
    {
        public Log(string desc)
        {
            Description = desc;
            Date = DateTime.Now;
        }

        public Log()
        {
        }

        public string Description { get; set; }
        public DateTime Date { get; set; }

        [Column("LogId")]
        public int Id { get; set; }

        public void setDescription(string desc)
        {
            Description = desc;
        }
    }
}