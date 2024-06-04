using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DemoApp.Helpers;
using DemoApp.Messager;
using System.Windows.Input;

namespace DemoApp.ViewModels
{
    public partial class CustomBottomSheetViewModel : ObservableObject
    {
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public CustomBottomSheetViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            SelectedIndex = SettingHelper.Enviroment.ToString();
        }

        [ObservableProperty]
        public string selectedIndex;

        private void Save()
        {
            if (int.TryParse(selectedIndex, out int value))
            {
                SettingHelper.Enviroment = value;
                Cancel();
            }
        }

        private void Cancel()
        {
            WeakReferenceMessenger.Default.Send<HideBottomSheetRequestMessage>();
        }
    }
}
