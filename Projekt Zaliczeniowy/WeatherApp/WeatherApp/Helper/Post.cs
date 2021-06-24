using System;
namespace WeatherApp.Helper
{
    public class Post
    {
        public string temperature { get; set; }
        public string descTemp { get; set; }
        public string humidity { get; set; }
        public string wind { get; set; }
        public string gauge { get; set; }
        public string cloudiness { get; set; }
        public DateTime dateCreated { get; set; }
        public string city { get; set; }
    }
}
