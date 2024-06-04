using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Models;
using DemoApp.Services;
using DemoApp.Validation.Customize;
using System.Windows.Input;

namespace DemoApp.ViewModels
{
    public partial class LoginValidationViewModel : ObservableObject
    {
        public ICommand ValidateEmailCommand { get; set; }
        public ICommand ValidatePasswordCommand { get; set; }
        public ICommand AuthenticateCommand { get; set; }
        private readonly IRestApiService _restApiService;
        private readonly INavigationService _navigateService;


        public LoginValidationViewModel(IRestApiService restApiService,
                                       INavigationService navigationService) 
        {
            ValidateEmailCommand = new RelayCommand(ValidateEmail);
            ValidatePasswordCommand = new RelayCommand(ValidatePassword);
            AuthenticateCommand = new AsyncRelayCommand(AuthenticateAsync);
            _restApiService = restApiService;
            _navigateService = navigationService;

            Email = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            InitValidation();
        }

        public ValidatableObject<string> Email { get; set; }
        public ValidatableObject<string> Password { get; set; }
        [ObservableProperty]
        public bool isValidated = false;
        private async Task AuthenticateAsync()
        {
            Uri uri = new Uri("https://dummyjson.com/auth/login");
            var result = await _restApiService.PostAsync<AuthenticateModel>(null, uri, new AuthenticateModel { Username = Email.Value, Password = Password.Value});
            if (result.IsSucceed)
            {
                await _navigateService.NavigateToAsync("//MainPage");
            }
        }
        private void ValidateEmail()
        {
            Email.Validate();
            ValidateInputs();
        }

        private void ValidatePassword()
        {
            Password.Validate();
            ValidateInputs();
        }

        private void ValidateInputs()
        {
            var isEmailValid = Email.IsValid && !Email.Errors.Any() && !string.IsNullOrEmpty(Email.Value);
            var isPasswordValid = Password.IsValid && !Password.Errors.Any() && !string.IsNullOrEmpty(Password.Value);
            IsValidated = isEmailValid && isPasswordValid;
        }

        private void InitValidation()
        {
            Email.Validations.Add(new StringRule<string>
            {
                ValidationMessage = "Email is not empty"
            });

            //Email.Validations.Add(new EmailRule<string>
            //{
            //    ValidationMessage = "Email not correct"
            //});

            Password.Validations.Add(new StringRule<string>
            {
                ValidationMessage = "Password is nor empty"
            });
        }
    }
}
