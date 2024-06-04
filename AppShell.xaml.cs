using DemoApp.Services;
using DemoApp.Views;
using Microsoft.Maui.Controls;

namespace DemoApp
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;
        public AppShell(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            RegisterRouteHandler();
        }
        protected override async void OnParentSet()
        {
            base.OnParentSet();

            if (Parent is not null)
            {
                await _navigationService.InitializeAsync();
            }
        }
        private void RegisterRouteHandler()
        {
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
           // Routing.RegisterRoute(nameof(LoginValidationPage), typeof(LoginValidationPage));
        }
    }
}
