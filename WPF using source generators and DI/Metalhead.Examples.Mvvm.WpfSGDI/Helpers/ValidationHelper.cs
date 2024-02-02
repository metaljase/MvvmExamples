using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Metalhead.Examples.Mvvm.WpfSGDI.Helpers;
internal static class ValidationHelper
{
    public static bool IsPropertyValid(object instance, string propertyName)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(instance);
        bool isValid = Validator.TryValidateObject(instance, validationContext, validationResults, true);

        if (!isValid)
        {
            return !validationResults.Any(vr => vr.MemberNames.Contains(propertyName));
        }

        return true;
    }
}
