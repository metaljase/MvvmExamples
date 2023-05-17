using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

public partial class NameViewModel : ObservableObject
{
    private readonly ILogger _logger;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _firstName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = "";

    public NameViewModel(ILogger logger)
    {
        _logger = logger;
    }

    public string FullName => $"{FirstName} {LastName}";

    [RelayCommand]
    void Reset()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        _logger.Log("Reset names");
    }
}
