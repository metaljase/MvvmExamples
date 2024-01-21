using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

public partial class NameViewModel(ILogger logger) : ObservableObject
{
    private readonly ILogger _logger = logger;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _firstName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = "";

    public string FullName => $"{FirstName} {LastName}".Trim();

    [RelayCommand]
    void Reset()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        _logger.Log("Reset names");
    }
}
