using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dyt_ecommerce.Models.ControllerModels.PagesControllerModels
{
    public class ResetPasswordModel : IValidatableObject
    {
        [Required, MinLength(4), MaxLength(128)]
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(NewPassword != ConfirmPassword)
            {
                yield return new ValidationResult("Passwords don't match!");
            }
        }
    }
}