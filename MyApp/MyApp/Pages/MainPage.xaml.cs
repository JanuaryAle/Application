using MyApp.Models;
using MyApp.Servises;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public const string DATABASE_NAME = "userData.db";
        private SetDataBaseConnection database;
        private List<Bank> banks;
        private List<Card> cards;
        private List<Operation> operations;
        private DateTime sTime;
        private DateTime eTime;
        public MainPage()
        {
            this.BindingContext = this;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.eTime = DateTime.Today;
            this.sTime = eTime.AddMonths(-1).AddYears(-4);
            endDate.Date = eTime;
            startDate.Date = sTime;
            Update();
        }
        public ObservableCollection<CardToPrint> CardForList { get => ConvertCard(); }
        private ObservableCollection<CardToPrint> ConvertCard()
        {
            var tempList = new ObservableCollection<CardToPrint>();
            foreach (Card card in Cards)
            {
                tempList.Add(new CardToPrint
                {
                    Name = card.Name + " *" + card.Number % 10000,
                    BankName = Database.BankNameByID(card),
                    Picture = "card" + (card.Number %10)%3 +".png"
                });
            }
            SetValuesOfCards(tempList);
            return tempList;
        }
        public ObservableCollection<OperationToPrint> OperationsForList { get => ConvertOperations(); }
        private ObservableCollection<OperationToPrint> ConvertOperations()
        {
            var tempList = new ObservableCollection<OperationToPrint>();
            foreach (Operation operation in Operations)
            {
                tempList.Add(new OperationToPrint
                {
                    CardName = Database.CardNameByID(operation),
                    Value = operation.Value + "р.",
                    Date = operation.Date.ToString()
                });
            }
            return tempList;
        }

        public SetDataBaseConnection Database
        {
            get
            {
                if (database == null)
                {
                    database = new SetDataBaseConnection(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public List<Bank> Banks
        {
            get
            {
                if (banks == null)
                {
                    banks = Database.GetBanks();
                }
                return banks;
            }
        }
        public List<Card> Cards
        {
            get
            {
                if (cards == null)
                {
                    cards = Database.GetCards();
                }
                return cards;
            }
        }
        public List<Operation> Operations
        {
            get
            {
                if (operations == null)
                {
                    operations = Database.GetOperations(sTime, eTime);
                }
                return operations;
            }
        }

        public class CardToPrint
        {
            public string Name { get; set; }
            public string BankName { get; set; }
            public string Value { get; set; }
            public string Picture { get; set; }
        }

        public class OperationToPrint
        {
            public string CardName { get; set; }
            public string Value { get; set; }
            public string Date { get; set; }
        }
        private void Button_Update(object sender, EventArgs e)
        {
            Update();
        }
        private void Update()
        {
            operations = null;
            cards = null;

            sTime = startDate.Date;
            eTime = endDate.Date;

            listView.ItemsSource = OperationsForList;
            carouselView.ItemsSource = CardForList;
            setTotal();
        }
        private void setTotal()
        {
            double total = 0;
            foreach (CardToPrint card in CardForList)
            {
                total += Convert.ToDouble(card.Value);
            }
            totalValue.Text = total + "";
            
        }
        private void SetValuesOfCards(ObservableCollection<CardToPrint> list)
        {
            var cardValues = new double[list.Count()];
            for (int i = 0; i < list.Count(); i++)
            {
                cardValues[i] = 0;
            }
            foreach (Operation oper in Operations)
            {
                cardValues[oper.CardID - 1] += oper.Value;
            }
            int index = 0;
            foreach (CardToPrint card in list)
            {
                card.Value = Convert.ToString(cardValues[index++]);
            }
        }
    }
}