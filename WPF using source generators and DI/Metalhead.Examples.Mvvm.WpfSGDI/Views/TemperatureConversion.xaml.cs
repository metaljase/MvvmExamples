using System.Windows.Controls;

using Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

namespace Metalhead.Examples.Mvvm.WpfSGDI.Views;

public partial class TemperatureConversion : UserControl
{
    public TemperatureConversion(TemperatureConversionViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
