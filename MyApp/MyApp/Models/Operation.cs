using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class Operation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int CardID { get; set; }
    }
}
