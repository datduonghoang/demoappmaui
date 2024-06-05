using DemoApp.ViewModels;

namespace DemoApp.Views;

public partial class LoginValidationMVVMPage : ContentPage
{
    LoginValidationMVVMViewModel? vm => BindingContext as LoginValidationMVVMViewModel;
    public LoginValidationMVVMPage(LoginValidationMVVMViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}