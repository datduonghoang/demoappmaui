using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public class NavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            return NavigateToAsync("//LoginValidationMVVMPage");
        }

        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {
            return routeParameters != null
            ? Shell.Current.GoToAsync(route, routeParameters)
            : Shell.Current.GoToAsync(route);
        }

        public Task PopAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
    }
}
