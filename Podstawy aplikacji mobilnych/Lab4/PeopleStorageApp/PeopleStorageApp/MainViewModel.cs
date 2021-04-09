using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PeopleStorageApp
{
    class MainViewModel : INotifyPropertyChanged
    {
        string name = string.Empty;
        string lastName = string.Empty;
        string phoneNumber = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                    return;

                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName == value)
                    return;

                lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(DisplayLastName));
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (phoneNumber == value)
                    return;

                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(DisplayPhoneNumber));
            }
        }

        public string DisplayName => $"First Name entered: {Name}";
        public string DisplayLastName => $"Last Name entered: {LastName}";
        public string DisplayPhoneNumber => $"Phone Number entered: {PhoneNumber}";

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
