using DemoApp.Services;
using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.Extensions
{
    public static class ServiceExtension
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IRestApiService, RestApiService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            return builder;
        }

        public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginValidationPage>();
            builder.Services.AddSingleton<LoginValidationMVVMPage>();


            return builder;
        }

        public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginValidationViewModel>();
            builder.Services.AddSingleton<LoginValidationMVVMViewModel>();

            return builder;
        }
    }
}
