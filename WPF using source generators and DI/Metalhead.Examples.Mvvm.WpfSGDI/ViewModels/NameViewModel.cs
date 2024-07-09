using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Threading.Tasks;

namespace Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

public partial class NameViewModel(ILogger logger) : ObservableObject
{
    private readonly ILogger _logger = logger;
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
        _logger.Log("Reset names");
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task Submit(CancellationToken token)
    {        
        IsBusy = true; // Alternatively, bind to SubmitCommand.IsRunning in the view (would also need to change BusySpinner control).
        _logger.Log($"Saving: {FullName}");

        try
        {
            // Mimic a long running operation, passing the token to allow for cancellation.
            await Task.Delay(5000, token);
            _logger.Log($"Saved!: {FullName}");
        }
        catch (TaskCanceledException)
        {
            _logger.Log($"Save canceled: {FullName}");
            return;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
