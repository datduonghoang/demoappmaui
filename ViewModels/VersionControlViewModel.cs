using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DemoApp.Controls;
using DemoApp.Messager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoApp.ViewModels
{
    public partial class VersionControlViewModel : ObservableRecipient
    {
        public ICommand ShowBottomSheetCommand { get; set; }
        private CustomBottomSheet Sheet { set; get; }
        public VersionControlViewModel()
        {
            Sheet = new CustomBottomSheet();
            ShowBottomSheetCommand = new AsyncRelayCommand(ShowBottomSheetAsync);
            WeakReferenceMessenger.Default.Register<HideBottomSheetRequestMessage>(this, (r, m) =>
            {
                Sheet.DismissAsync();
            });
        }
        protected override void OnDeactivated()
        {
        }
        [ObservableProperty]
        public string version = AppInfo.Current.VersionString;

        private async Task ShowBottomSheetAsync()
        {
            await Sheet.ShowAsync();
        }
    }
}
