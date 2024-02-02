using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.ComponentModel.DataAnnotations;

using Metalhead.Examples.Mvvm.WpfSG.Helpers;

namespace Metalhead.Examples.Mvvm.WpfSG.ViewModels;

public partial class TemperatureConversionViewModel : ObservableValidator
{
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

    public TemperatureConversionViewModel()
    {
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

    public bool CelsiusHasNoErrors => ValidationHelper.IsPropertyValid(this, nameof(Celsius));
    public bool FahrenheitHasNoErrors => ValidationHelper.IsPropertyValid(this, nameof(Fahrenheit));

    [RelayCommand(CanExecute = nameof(FahrenheitHasNoErrors))]
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

    [RelayCommand(CanExecute = nameof(CelsiusHasNoErrors))]
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
