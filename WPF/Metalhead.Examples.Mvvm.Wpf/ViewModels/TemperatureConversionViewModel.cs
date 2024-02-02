using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.ComponentModel.DataAnnotations;

using Metalhead.Examples.Mvvm.Wpf.Helpers;

namespace Metalhead.Examples.Mvvm.Wpf.ViewModels;

public partial class TemperatureConversionViewModel : ObservableValidator
{
    private string _celsius = string.Empty;
    private string _fahrenheit = string.Empty;

    public TemperatureConversionViewModel()
    {
        ToCelsiusCommand = new RelayCommand(ToCelsius, () => FahrenheitHasNoErrors);
        ToFahrenheitCommand = new RelayCommand(ToFahrenheit, () => CelsiusHasNoErrors);

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

    [Range(-273.15, int.MaxValue, ErrorMessage = "Only values between {1} and {2} are allowed.")]
    public string Celsius
    {
        get => _celsius;
        set
        {
            if (SetProperty(ref _celsius, value, validate: true))
            {
                OnPropertyChanged(nameof(Celsius));
                OnPropertyChanged(nameof(CelsiusHasNoErrors));
            }
        }
    }

    [Range(-459.67, int.MaxValue, ErrorMessage = "Only values between {1} and {2} are allowed.")]
    public string Fahrenheit
    {
        get => _fahrenheit;
        set
        {
            if (SetProperty(ref _fahrenheit, value, validate: true))
            {
                OnPropertyChanged(nameof(Fahrenheit));
                OnPropertyChanged(nameof(FahrenheitHasNoErrors));
            }
        }
    }

    public bool CelsiusHasNoErrors => ValidationHelper.IsPropertyValid(this, nameof(Celsius));
    public bool FahrenheitHasNoErrors => ValidationHelper.IsPropertyValid(this, nameof(Fahrenheit));

    private void ToCelsius()
    {
        try
        {
            Celsius = TemperatureHelper.FahrenheitToCelsius(Fahrenheit).ToString();
        }
        catch (FormatException)
        {
            Celsius = string.Empty;
        }
        finally
        {
            OnPropertyChanged(nameof(Celsius));
        }
    }

    private void ToFahrenheit()
    {
        try
        {
            Fahrenheit = TemperatureHelper.CelsiusToFahrenheit(Celsius).ToString();
        }
        catch (FormatException)
        {
            Fahrenheit = string.Empty;
        }
        finally
        {
            OnPropertyChanged(nameof(Fahrenheit));
        }
    }
}
