using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Metalhead.Examples.Mvvm.Wpf.ViewModels;

public partial class NameViewModel : ObservableObject
{
    private string _firstName;
    private string _lastName;

    public NameViewModel()
    {
        _firstName = string.Empty;
        _lastName = string.Empty;
        SubmitCommand = new RelayCommand(Submit);
    }

    public RelayCommand SubmitCommand { get; }

    public string FirstName
    {
        get => _firstName;
        set
        {
            SetProperty(ref _firstName, value);
            OnPropertyChanged(nameof(FullName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            SetProperty(ref _lastName, value);
            OnPropertyChanged(nameof(FullName));
        }
    }

    public string FullName => $"{FirstName} {LastName}";

    private void Submit()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }
}
