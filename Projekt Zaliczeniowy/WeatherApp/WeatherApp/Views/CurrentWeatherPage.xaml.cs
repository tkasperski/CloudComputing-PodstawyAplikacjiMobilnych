using WeatherApp.Helper;
using WeatherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net.Http;
using RestSharp;
using WeatherApp.Helper;
using SQLite;
using System.IO;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentWeatherPage : ContentPage
    {
        public CurrentWeatherPage()
        {
            InitializeComponent();
            GetCoordinates();
            //GetWeatherInfo();
        }

        // Setting location variables
        private string Location { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Getting coordinates
        private async void GetCoordinates()
        {
            try
            {
                // Send request for geolocation with best accurancy
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                if(location != null)
                {
                    // Set location to variable
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                    Location = await GetCity(location);

                    // When we have location then get weather info from API
                    GetWeatherInfo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private async Task<string> GetCity(Location location)
        {
            // Get city name from geolocation
            var places = await Geocoding.GetPlacemarksAsync(location);
            var currentPlace = places?.FirstOrDefault();

            if (currentPlace != null)
                return $"{currentPlace.Locality},{currentPlace.CountryName}";

            return null;
        }

        // Get inforamtion about weather from API and bind view with it
        private async void GetWeatherInfo()
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Location}&appid=1766bbcd89f31e1461fcfd6f7d026204&units=metric";

            // Request for data
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    // If request is successful deserialize object to variable
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);

                    // Bind with information from variable
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    //iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    // Formatting date according to API documentation (Unix, UTC)
                    var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();

                    // When we have weather data then get forecast
                    GetForecast();
                }
                catch (Exception ex)
                {
                    // Display message as an alert
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                // Display message as an alert
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }
        }

        private async void GetForecast()
        {
            // Api call
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={Location}&appid=1766bbcd89f31e1461fcfd6f7d026204&units=metric";
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    // If request is successful deserialize object to variable
                    var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    // Create list for information for four days in one "place"
                    List<List> allList = new List<List>();

                    foreach (var list in forcastInfo.list)
                    {
                        //var date = DateTime.ParseExact(list.dt_txt, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                        var date = DateTime.Parse(list.dt_txt);

                        // Check date and if everything is fine add to list
                        if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    // Binding forecast with data
                    dayOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd");
                    dateOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                    //iconOneImg.Source = $"w{allList[0].weather[0].icon}";
                    tempOneTxt.Text = allList[0].main.temp.ToString("0");

                    dayTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                    dateTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    //iconTwoImg.Source = $"w{allList[1].weather[0].icon}";
                    tempTwoTxt.Text = allList[1].main.temp.ToString("0");

                    dayThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                    dateThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    //iconThreeImg.Source = $"w{allList[2].weather[0].icon}";
                    tempThreeTxt.Text = allList[2].main.temp.ToString("0");

                    dayFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                    dateFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    //iconFourImg.Source = $"w{allList[3].weather[0].icon}";
                    tempFourTxt.Text = allList[3].main.temp.ToString("0");

                }
                catch (Exception ex)
                {
                    // Display message as an alert
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                // Display message as an alert
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }

        private static readonly HttpClient client = new HttpClient();

        // Obsluga przycisku Save Weather
        async void OnWeatherSave(object sender, EventArgs args)
        {
            string url = "http://192.168.1.89:5000/api/WeatherInfoes1";

            var client = new RestClient(url);

            var request = new RestRequest();

            var body = new Post
            {
                temperature = temperatureTxt.Text,
                descTemp = descriptionTxt.Text,
                humidity = humidityTxt.Text,
                wind = windTxt.Text,
                gauge = pressureTxt.Text,
                cloudiness = cloudinessTxt.Text,
                dateCreated = DateTime.Now,
                city = cityTxt.Text
            };

            request.AddJsonBody(body);

            var response = client.Post(request);

            Console.WriteLine(response.StatusCode.ToString() + "   " + response.Content.ToString());

            Console.WriteLine("Dane wyslane");

            // Zapis do lokalnej bazy
            await Init();

            var localWeather = new LocalWeather
            {
                Id = 0,
                temperature = temperatureTxt.Text,
                descTemp = descriptionTxt.Text,
                humidity = humidityTxt.Text,
                wind = windTxt.Text,
                gauge = pressureTxt.Text,
                cloudiness = cloudinessTxt.Text,
                dateCreated = DateTime.Now,
                city = cityTxt.Text
            };

            await db.InsertAsync(localWeather);

            Console.WriteLine("Dane zapisane w lokalnej bazie");
        }

        // Obsluga przycisku My Favourites
        async void OnMyFavourites(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SavedWeatherPage());
        }

        // Obsluga lokalnej bazy danych SQLite
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;
            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<LocalWeather>();
        }

    }
}