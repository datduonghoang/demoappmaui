using DemoApp.ViewModels;

namespace DemoApp.Controls;

public partial class VersionControl : ContentView
{
    public VersionControl()
	{
		InitializeComponent();
        BindingContext = new VersionControlViewModel();

    }
}