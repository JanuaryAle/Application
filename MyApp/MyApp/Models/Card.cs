using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class Card
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public long Number { get; set; }

        [MaxLength(8)]
        public string Name { get; set; }

        [MaxLength(8)]
        public int BankID { get; set; }

    }
}
