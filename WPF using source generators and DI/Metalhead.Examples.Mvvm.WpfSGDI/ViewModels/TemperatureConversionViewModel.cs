using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

public partial class TemperatureConversionViewModel : ObservableObject
{
    private readonly ILogger _logger;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Conversion))]
    private string _temperature = string.Empty;

    public TemperatureConversionViewModel(ILogger logger)
    {
        _logger = logger;
        Conversion = string.Empty;
    }

    public string Conversion { get; private set; }

    [RelayCommand]
    void ToCelsius()
    {
        if (int.TryParse(Temperature, out int temperature))
        {
            Conversion = $"{Temperature} Fahrenheit = {(temperature - 32) * 5 / 9} Celsius";
        }
        else
        {
            Conversion = string.Empty;
        }

        OnPropertyChanged(nameof(Conversion));
        _logger.Log(Conversion);
    }

    [RelayCommand]
    void ToFahrenheit()
    {
        if (int.TryParse(Temperature, out int temperature))
        {
            Conversion = $"{Temperature} Celsius = {temperature * 9 / 5 + 32} Fahrenheit";
        }
        else
        {
            Conversion = string.Empty;
        }

        OnPropertyChanged(nameof(Conversion));
        _logger.Log(Conversion);
    }
}
