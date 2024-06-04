using DemoApp.Enums;

namespace DemoApp.Helpers
{
    public class SettingHelper
    {
        public static int Enviroment
        {
            get => Preferences.Get(nameof(Enviroment), (int)EnviromentType.Production);
            set => Preferences.Set(nameof(Enviroment), value);
        }
    }
}
