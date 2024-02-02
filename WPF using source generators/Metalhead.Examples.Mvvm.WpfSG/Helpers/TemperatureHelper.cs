using System;

namespace Metalhead.Examples.Mvvm.WpfSG.Helpers;

internal static class TemperatureHelper
{
    internal static double CelsiusToFahrenheit(string celsius)
    {
        var conversion = Math.Round(double.Parse(celsius) * 9 / 5 + 32, 2);
        return conversion < -459.67 ? -459.67 : conversion;
    }

    internal static double FahrenheitToCelsius(string fahrenheit)
    {
        var conversion = Math.Round((double.Parse(fahrenheit) - 32) * 5 / 9, 2);
        return conversion < -273.15 ? -273.15 : conversion;
    }
}