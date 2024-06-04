using CommunityToolkit.Maui;
using DemoApp.Extensions;
using Microsoft.Extensions.Logging;
using The49.Maui.BottomSheet;
using UraniumUI;

namespace DemoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                 .UseMauiCommunityToolkit()
                 .UseBottomSheet()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureServices()
                .ConfigureViewModels()
                .ConfigurePages()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
