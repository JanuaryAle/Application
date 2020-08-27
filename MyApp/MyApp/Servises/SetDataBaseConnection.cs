using MyApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Servises
{
    public class SetDataBaseConnection
    {
        private SQLiteConnection db;
        public SetDataBaseConnection(String path)
        {
            db = new SQLiteConnection(path);
            db.CreateTable<Card>();
            db.CreateTable<Bank>();
            db.CreateTable<Operation>();
            if (GetBanks().Count == 0)
            {
                LoadData();
            }
        }

        public void AddBank(Bank bank)
        {
            db.Insert(bank);
        }
        public void AddCard(Card card)
        {
            db.Insert(card);
        }
        public void AddOperation(Operation operation)
        {
            db.Insert(operation);
        }

        public List<Card> GetCards()
        {
            var cards = db.Table<Card>().OrderBy(p => p.ID);
            return cards.ToList();
        }
        public List<Bank> GetBanks()
        {
            var banks = db.Table<Bank>().OrderBy(p => p.ID);
            return banks.ToList();
        }
        public List<Operation> GetOperations(DateTime start, DateTime end)
        {
            var operations = db.Query<Operation>("SELECT * FROM operation WHERE date >= ? AND date <= ? ORDER BY date", start.Ticks, end.Ticks);
            return operations;
        }

        public void LoadData()
        {
            AddBank(new Bank { Name = "Tinkoff" });
            AddBank(new Bank { Name = "Sberbank" });
            AddCard(new Card { Name = "Visa Classic", BankID = 1, Number = 7777_7777_7777_7777 });
            AddCard(new Card { Name = "Gold Card", Number = 8888_9999_4565_1321, BankID = 2 });
            for (int i = 1; i <= 36; i++)
            {
                AddOperation(new Operation { CardID = 2, Date = DateTime.Today.AddMonths(-i), Value = i * 10 });
            }
            for (int i = 1; i <= 20; i++)
            {
                AddOperation(new Operation { CardID = 1, Date = DateTime.Today.AddMonths(-i).AddDays(-i), Value = i * 10 });
            }
        }

        public string BankNameByID(Card card)
        {
            var bankList = db.Query<Bank>("SELECT Name FROM bank WHERE ID = ? ",card.BankID);
            string bankName = bankList[0].Name;
            return bankName;
        }
        public string CardNameByID(Operation operation)
        {
            var cardList = db.Query<Bank>("SELECT Name FROM card WHERE ID = ? ", operation.CardID);
            string cardName = cardList[0].Name;
            return cardName;
        }

    }
}
