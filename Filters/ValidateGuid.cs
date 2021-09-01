using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Filters
{
    /// <summary>
    /// Validate the GUID
    /// </summary>
    public class ValidateGuid : ValidationAttribute
    {
        /// <summary>
        /// IsValid override validation
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return System.Guid.TryParse(value.ToString(), out var guid) ? ValidationResult.Success : new ValidationResult("Invalid GUID input.");
        }
    }
}
