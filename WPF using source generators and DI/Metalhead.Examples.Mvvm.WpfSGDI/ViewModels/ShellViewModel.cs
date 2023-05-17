using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.WpfSGDI.Views;

namespace Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl? _currentView;
    private readonly ILogger _logger;

    public ShellViewModel(ILogger logger)
    {
        _logger = logger;
        _currentView = null;
    }

    [RelayCommand]
    private void ShowTemperatureView()
    {
        CurrentView = App.Current.Host.Services.GetService<TemperatureConversion>();
        _logger.Log("Show Temperature view");
    }

    [RelayCommand]
    private void ShowNameView()
    {
        CurrentView = App.Current.Host.Services.GetService<Name>();
        _logger.Log("Show Name view");
    }
}
