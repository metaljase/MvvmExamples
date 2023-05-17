using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Metalhead.Examples.Mvvm.Wpf.ViewModels;

public partial class TemperatureConversionViewModel : ObservableObject
{
    private string _temperature = string.Empty;

    public TemperatureConversionViewModel()
    {
        Conversion = string.Empty;
        ToCelsiusCommand = new RelayCommand(ToCelsius);
        ToFahrenheitCommand = new RelayCommand(ToFahrenheit);
    }

    public RelayCommand ToCelsiusCommand { get; }
    public RelayCommand ToFahrenheitCommand { get; }

    public string Temperature
    {
        get => _temperature;
        set
        {
            SetProperty(ref _temperature, value);
            OnPropertyChanged(nameof(Conversion));
        }
    }

    public string Conversion { get; private set; }

    private void ToCelsius()
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
    }

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
    }
}
