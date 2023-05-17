using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.WpfSG.Views;

namespace Metalhead.Examples.Mvvm.WpfSG.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl? _currentView;

    public ShellViewModel()
    {
        _currentView = null;
    }

    [RelayCommand]
    private void ShowTemperatureView()
    {
        CurrentView = new TemperatureConversion();
    }

    [RelayCommand]
    private void ShowNameView()
    {
        CurrentView = new Name();
    }
}
