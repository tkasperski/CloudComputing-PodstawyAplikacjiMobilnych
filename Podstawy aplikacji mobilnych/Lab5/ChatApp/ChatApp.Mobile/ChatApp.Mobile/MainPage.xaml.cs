using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp.Mobile
{
 public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            btnLogin.Clicked += btnLogin_Clicked;
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var userName = txtLogin.Text;
            if (string.IsNullOrEmpty(userName))
            {
                await DisplayAlert("Error", "Empty username", "OK");
                return;
            }

            await Navigation.PushAsync(new ChatPage(userName));

        }
    }
}
