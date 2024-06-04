using DemoApp.ViewModels;
using The49.Maui.BottomSheet;

namespace DemoApp.Controls;

public partial class CustomBottomSheet : BottomSheet
{
	public CustomBottomSheet()
	{
		InitializeComponent();
		BindingContext = new CustomBottomSheetViewModel();
	}
}