using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Metalhead.Examples.Mvvm.WpfSG.ViewModels;

public partial class NameViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _firstName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = "";

    public string FullName => $"{FirstName} {LastName}".Trim();

    [RelayCommand]
    void Submit()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }
}
