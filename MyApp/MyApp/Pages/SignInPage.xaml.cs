using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public string nameFile;
        private Dictionary<string, string> Users = new Dictionary<string, string>();
        public SignInPage()
        {
            Users.Add("Sasha", "123qwe");
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            EntryPass.Text = "";
            EntryName.Text = "";
            EntryPass.Placeholder = "Password";
            EntryName.Placeholder = "User name";
            label.IsVisible = false;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {

            if (Users.ContainsKey(EntryName.Text) && (Users[EntryName.Text] == EntryPass.Text))
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                label.IsVisible = true;
            }
            EntryPass.Text = "";
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            await DisplayAlert("Sign up", "Сервис пока недоступен", "Ok");
        }
    }
}