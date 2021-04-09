using PeopleStorageApp.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PeopleStorageApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly IPeopleClient client;
        private Person person = new Person();
        public MainPage(IPeopleClient client)
        {
            InitializeComponent();
            IntializeEvents();
            this.client = client;
        }

        private void IntializeEvents()
        {
            btnSave.Clicked += async (object sender, EventArgs e) =>
            {
                if (!Validate()) 
                {
                    await DisplayAlert("Validation Error", "First name, last name, phone number and picture are required.", "Ok");
                    return;
                }

                try
                {
                    await client.AddPersonAsync(person);
                    await DisplayAlert("Success", "Data has been saved.", "Ok");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Ok");
                }

            };

            btnCamera.Clicked += async (object sender, EventArgs e) =>
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {

                });

                if (photo != null)
                {
                    imgPhoto.Source = ImageSource.FromStream(() => photo.GetStream());
                    byte[] bytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        photo.GetStream().CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                    }

                    string base64 = Convert.ToBase64String(bytes);
                    person.PictureBase64 = base64;
                }
            };

            entFirstName.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                person.FirstName = e.NewTextValue;
            };

            entLastName.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                person.LastName = e.NewTextValue;
            };

            entPhoneNumber.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                person.PhoneNumber = e.NewTextValue;
            };
        }


        private bool Validate()
        {
            return !(string.IsNullOrWhiteSpace(person.FirstName) ||
                    string.IsNullOrWhiteSpace(person.LastName) ||
                    string.IsNullOrWhiteSpace(person.PhoneNumber) ||
                    string.IsNullOrWhiteSpace(person.PictureBase64)
                );
        }

    }
}
