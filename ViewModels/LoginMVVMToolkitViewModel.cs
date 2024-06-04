using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace DemoApp.ViewModels
{
    public partial class LoginMVVMToolkitViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AuthenticateCommand))]
        public string email;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AuthenticateCommand))]
        public string password;

        public IRelayCommand AuthenticateCommand { get; set; }

        public LoginMVVMToolkitViewModel()
        {
            AuthenticateCommand = new RelayCommand(Authenticate,() => CanExcute);
        }

        private void Authenticate()
        {
            Debug.WriteLine($"Email input: {Email}");
            Debug.WriteLine($"Password input: {Password}");
        }
        private bool CanExcute => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
    }
}
