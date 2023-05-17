using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

namespace Metalhead.Examples.Mvvm.WpfSGDI.Views;

public partial class TemperatureConversion : UserControl
{
    public TemperatureConversion()
    {
        InitializeComponent();
        DataContext = App.Current.Host.Services.GetService<TemperatureConversionViewModel>();
    }
}
