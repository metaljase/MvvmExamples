using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Metalhead.Examples.Mvvm.Wpf.ViewModels;

public partial class NameViewModel : ObservableObject
{
    private bool _isBusy;
    private string _firstName;
    private string _lastName;
    private ICommand? _submitCancelCommand;

    public NameViewModel()
    {
        _isBusy = false;
        _firstName = string.Empty;
        _lastName = string.Empty;
        ResetCommand = new RelayCommand(Reset);
        SubmitCommand = new AsyncRelayCommand(Submit);
    }

    public RelayCommand ResetCommand { get; }
    public AsyncRelayCommand SubmitCommand { get; }
    public ICommand? SubmitCancelCommand => _submitCancelCommand ??= SubmitCommand.CreateCancelCommand();

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (SetProperty(ref _firstName, value))
                OnPropertyChanged(nameof(FullName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (SetProperty(ref _lastName, value))
                OnPropertyChanged(nameof(FullName));
        }
    }

    public string FullName => $"{FirstName} {LastName}".Trim();

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy == value)
                return;
            _isBusy = value;
            OnPropertyChanged(nameof(IsBusy));
        }
    }

    private void Reset()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    private async Task Submit(CancellationToken token)
    {
        IsBusy = true; // Alternatively, bind to SubmitCommand.IsRunning in the view (would also need to change BusySpinner control).

        try
        {
            // Mimic a long running operation, passing the token to allow for cancellation.
            await Task.Delay(5000, token);
        }
        catch (TaskCanceledException)
        {
            return;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
