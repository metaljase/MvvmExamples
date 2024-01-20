using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.WpfSGDI.Views;

namespace Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

public partial class ShellViewModel(ILogger logger) : ObservableObject
{
    [ObservableProperty]
    private UserControl? _currentView = null;
    private readonly ILogger _logger = logger;

    partial void OnCurrentViewChanged(UserControl? value)
    {
        // Tell registered recipients the view has changed.
        if (value is not null)
        {
            WeakReferenceMessenger.Default.Send(new ChangedViewMessage(value));
        }        
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
