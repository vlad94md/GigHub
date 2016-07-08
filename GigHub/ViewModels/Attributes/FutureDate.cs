using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels.Attributes
{
    public class FutureDate : ValidationAttribute
    {

        private readonly string[] _formats = {"M/d/yyyy", "M/d/yyyy",
                         "MM/dd/yyyy", "M/d/yyyy ",
                         "M/d/yyyy", "M/d/yyyy",
                         "M/d/yyyy", "M/d/yyyy",
                         "MM/dd/yyyy", "M/dd/yyyy"};


        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                _formats, 
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, 
                out dateTime);

            return (isValid && dateTime > DateTime.Now);
        }
    }
}