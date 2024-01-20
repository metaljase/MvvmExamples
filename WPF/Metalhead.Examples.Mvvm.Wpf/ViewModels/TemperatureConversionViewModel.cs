using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel.DataAnnotations;

namespace Metalhead.Examples.Mvvm.Wpf.ViewModels;

public partial class TemperatureConversionViewModel : ObservableValidator
{
    private string _temperature = string.Empty;

    public TemperatureConversionViewModel()
    {
        Conversion = string.Empty;
        ToCelsiusCommand = new RelayCommand(ToCelsius);
        ToFahrenheitCommand = new RelayCommand(ToFahrenheit);

        // Register to recieve a message when the view has been (re)displayed.
        WeakReferenceMessenger.Default.Register<ChangedViewMessage>(this, (r, m) =>
        {
            // Switching between views can cause the view's visual tree to detach.  This can result in views not
            // correctly redisplaying validation errors when validation errors existed before switching views.
            // Clearing the validation errors and revaliding the properties will redisplay any validation errors.
            if (HasErrors)
            {
                ClearErrors();
                ValidateAllProperties();
            }
        });
    }

    public RelayCommand ToCelsiusCommand { get; }
    public RelayCommand ToFahrenheitCommand { get; }

    [Required(ErrorMessage = "Temperature is required.")]
    [RegularExpression("^-?[0-9]*$", ErrorMessage = "Only numeric values are allowed.")]
    public string Temperature
    {
        get => _temperature;
        set
        {
            if (SetProperty(ref _temperature, value, validate: true))
            {
                OnPropertyChanged(nameof(Conversion));
                OnPropertyChanged(nameof(HasNoErrors));
            }
        }
    }

    public string Conversion { get; private set; }
    public bool HasNoErrors => !HasErrors;

    private void ToCelsius()
    {
        // As a precaution, perform final check for any validation errors before conversion.
        ValidateAllProperties();

        if (!HasErrors && int.TryParse(Temperature, out int temperature))
        {
            Conversion = $"{Temperature} Fahrenheit = {(temperature - 32) * 5 / 9} Celsius";
        }
        else
        {
            Conversion = string.Empty;
        }

        OnPropertyChanged(nameof(Conversion));
    }

    void ToFahrenheit()
    {
        // As a precaution, perform final check for any validation errors before conversion.
        ValidateAllProperties();

        if (!HasErrors && int.TryParse(Temperature, out int temperature))
        {
            Conversion = $"{Temperature} Celsius = {temperature * 9 / 5 + 32} Fahrenheit";
        }
        else
        {
            Conversion = string.Empty;
        }

        OnPropertyChanged(nameof(Conversion));
    }
}
