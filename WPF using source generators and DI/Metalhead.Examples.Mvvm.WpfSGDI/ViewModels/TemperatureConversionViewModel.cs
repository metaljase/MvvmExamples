using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.ComponentModel.DataAnnotations;

using Metalhead.Examples.Mvvm.WpfSGDI.Helpers;

namespace Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

public partial class TemperatureConversionViewModel : ObservableValidator
{
    private readonly ILogger _logger;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(-273.15, int.MaxValue, ErrorMessage = "Only values between {1} and {2} are allowed.")]
    [NotifyPropertyChangedFor(nameof(CelsiusHasNoErrors))]
    private string _celsius = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(-459.67, int.MaxValue, ErrorMessage = "Only values between {1} and {2} are allowed.")]
    [NotifyPropertyChangedFor(nameof(FahrenheitHasNoErrors))]
    private string _fahrenheit = string.Empty;

    public TemperatureConversionViewModel(ILogger logger)
    {
        _logger = logger;

        // Register to receive a message when the view has been (re)displayed.
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

    public bool CelsiusHasNoErrors => ValidationHelper.IsPropertyValid(this, nameof(Celsius));
    public bool FahrenheitHasNoErrors => ValidationHelper.IsPropertyValid(this, nameof(Fahrenheit));

    [RelayCommand(CanExecute = nameof(FahrenheitHasNoErrors))]
    void ToCelsius()
    {
        try
        {
            var conversion = TemperatureHelper.FahrenheitToCelsius(Fahrenheit);
            var suffix = conversion <= -273.15 ? " (absolute zero)" : string.Empty;
            Celsius = conversion.ToString();
            _logger.Log($"{Fahrenheit} = {Celsius}{suffix}");
        }
        catch (FormatException ex)
        {
            Celsius = string.Empty;
            _logger.Log(ex.Message);
        }
        finally
        {
            OnPropertyChanged(nameof(Celsius));
        }
    }

    [RelayCommand(CanExecute = nameof(CelsiusHasNoErrors))]
    void ToFahrenheit()
    {
        try
        {
            var conversion = TemperatureHelper.CelsiusToFahrenheit(Celsius);
            var suffix = conversion <= -459.67 ? " (absolute zero)" : string.Empty;
            Fahrenheit = conversion.ToString();
            _logger.Log($"{Celsius} = {Fahrenheit}{suffix}");
        }
        catch (FormatException ex)
        {
            Fahrenheit = string.Empty;
            _logger.Log(ex.Message);
        }
        finally
        {
            OnPropertyChanged(nameof(Fahrenheit));
        }
    }
}
