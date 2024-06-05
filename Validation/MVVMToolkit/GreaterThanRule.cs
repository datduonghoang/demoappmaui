using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Validation.MVVMToolkit
{
    public sealed class GreaterThanAttribute : ValidationAttribute
    {
        public GreaterThanAttribute(int max)
        {
            MaxLength = max;
        }

        public int MaxLength { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString().Length >= MaxLength)
            {
                return ValidationResult.Success;
            }

            return new($"The Password must have length greater than {MaxLength}");
        }
    }

}
