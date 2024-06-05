using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Models;
using DemoApp.Services;
using DemoApp.Validation.MVVMToolkit;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace DemoApp.ViewModels
{
    public partial class LoginValidationMVVMViewModel : ObservableValidator
    {
        public ICommand AuthenticateCommand { get; set; }

        [ObservableProperty]
        [Required]
        [EmailAddress]
        public string email;

        [ObservableProperty]
        [Required]
        [GreaterThan(10)]
        public string password;
        [ObservableProperty]
        public string errorMessage;

        private readonly IRestApiService _restApiService;
        private readonly INavigationService _navigateService;

        private async Task AuthenticateAsync()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                ErrorMessage = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            }
            else
            {
                Uri uri = new Uri("https://dummyjson.com/auth/login");
                var result = await _restApiService.PostAsync<AuthenticateModel>(null, uri, new AuthenticateModel { Username = Email, Password = Password });
                if (result.IsSucceed)
                {
                    await _navigateService.NavigateToAsync("//MainPage");
                }
            }
        }

        public LoginValidationMVVMViewModel(IRestApiService restApiService,
                                       INavigationService navigationService)
        {
            AuthenticateCommand = new AsyncRelayCommand(AuthenticateAsync);
            _restApiService = restApiService;
            _navigateService = navigationService;
        }
    }
}
