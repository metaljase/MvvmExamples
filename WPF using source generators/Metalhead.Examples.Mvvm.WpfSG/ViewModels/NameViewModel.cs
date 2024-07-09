using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Threading.Tasks;

namespace Metalhead.Examples.Mvvm.WpfSG.ViewModels;

public partial class NameViewModel : ObservableObject
{
    private bool _isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _firstName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = "";

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

    [RelayCommand]
    private void Reset()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    [RelayCommand(IncludeCancelCommand = true)]
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
