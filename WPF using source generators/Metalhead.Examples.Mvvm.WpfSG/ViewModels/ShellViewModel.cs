using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.WpfSG.Views;

namespace Metalhead.Examples.Mvvm.WpfSG.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl? _currentView;
    private readonly UserControl _nameView;
    private readonly UserControl _temperatureView;

    public ShellViewModel()
    {
        _currentView = null;
        _nameView = new Name();
        _temperatureView = new TemperatureConversion();
    }

    public UserControl NameView { get => _nameView; }
    public UserControl TemperatureView { get => _temperatureView; }
    
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
        CurrentView = TemperatureView;
    }

    [RelayCommand]
    private void ShowNameView()
    {
        CurrentView = NameView;
    }
}
