using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Entity
{
    public class Employee : IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [BindRequired]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Annual salary is not valid")]
        public decimal AnnualSalary { get; set; }
        [BindRequired]
        [Range(0, 0.50, ErrorMessage = "Super Rate is not valid")]
        public decimal SuperRate { get; set; }
        [BindRequired]
        public DateTime PaymentStartDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (PaymentStartDate < new DateTime(1900, 1, 1))
            {
                results.Add(new ValidationResult("Payment Start date is not valid", new[] { "PaymentStartDate" }));
            }

            return results;
        }
    }
}
