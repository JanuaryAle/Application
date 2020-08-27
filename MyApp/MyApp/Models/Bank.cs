using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class Bank
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
