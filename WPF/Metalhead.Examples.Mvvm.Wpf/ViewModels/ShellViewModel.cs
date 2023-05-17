using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.Wpf.Views;

namespace Metalhead.Examples.Mvvm.Wpf.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private UserControl? _currentView;

    public ShellViewModel()
    {
        _currentView = null;
        ShowTemperatureViewCommand = new RelayCommand(ShowTemperatureView);
        ShowNameViewCommand = new RelayCommand(ShowNameView);
    }

    public RelayCommand ShowTemperatureViewCommand { get; }
    public RelayCommand ShowNameViewCommand { get; }

    public UserControl? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    private void ShowTemperatureView()
    {
        CurrentView = new TemperatureConversion();
    }

    private void ShowNameView()
    {
        CurrentView = new Name();
    }
}
