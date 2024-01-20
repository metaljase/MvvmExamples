using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.Wpf.Views;

namespace Metalhead.Examples.Mvvm.Wpf.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private UserControl? _currentView;
    private readonly UserControl _nameView;
    private readonly UserControl _temperatureView;

    public ShellViewModel()
    {
        _currentView = null;
        _nameView = new Name();
        _temperatureView = new TemperatureConversion();
        ShowTemperatureViewCommand = new RelayCommand(ShowTemperatureView);
        ShowNameViewCommand = new RelayCommand(ShowNameView);
    }

    public UserControl NameView { get => _nameView; }
    public UserControl TemperatureView { get => _temperatureView; }

    public RelayCommand ShowTemperatureViewCommand { get; }
    public RelayCommand ShowNameViewCommand { get; }

    public UserControl? CurrentView
    {
        get => _currentView;
        set
        {
            SetProperty(ref _currentView, value);

            // Tell registered recipients the view has changed.
            if (value is not null)
            {
                WeakReferenceMessenger.Default.Send(new ChangedViewMessage(value));
            }
        }
    }

    private void ShowTemperatureView()
    {
        CurrentView = TemperatureView;
    }

    private void ShowNameView()
    {
        CurrentView = NameView;
    }
}
