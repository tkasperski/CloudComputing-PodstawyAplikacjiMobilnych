using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using WeatherApp.Helper;
using System.Linq;

namespace WeatherApp.Views
{
    public partial class SavedWeatherPage : ContentPage
    {
        public SavedWeatherPage()
        {
            InitializeComponent();
            GetData();
        }

        async void GetData()
        {
            var httpClient = new HttpClient();
            var resultJson = await httpClient.GetStringAsync("http://192.168.1.89:5000/api/WeatherInfoes1");

            var resultSaved = JsonConvert.DeserializeObject<Post[]>(resultJson);

            weatherList.ItemsSource = resultSaved;

            Console.WriteLine("dane pobrane");
        }
    }
}
