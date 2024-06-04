using DemoApp.ViewModels;

namespace DemoApp.Views;

public partial class LoginValidationPage : ContentPage
{
    LoginValidationViewModel? vm => BindingContext as LoginValidationViewModel;
    public LoginValidationPage(LoginValidationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}