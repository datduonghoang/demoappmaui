using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DemoApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand AuthenticateCommand { set; get; }
        private string _email;
        private string _password;
        public LoginViewModel()
        {
            AuthenticateCommand = new Command(
            execute: () =>
            {
                //call Api
                Debug.WriteLine($"Email input: {Email}");
                Debug.WriteLine($"Password input: {Password}");
            },
            canExecute: () =>
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            });
        }
        public string Email
        {
            get => _email;
            set => SetPropertyValue(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetPropertyValue(ref _password, value);
        }

        protected void SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storageField, newValue))
                return;

            storageField = newValue;
            RaisePropertyChanged(propertyName);
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            (AuthenticateCommand as Command).ChangeCanExecute();
        }
    }
}
