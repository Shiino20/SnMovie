using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snmovie.Models
{
  public class Min18YearsForMembership : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var customer = (Customer)validationContext.ObjectInstance;

      if (customer.MembershipTypeId == MembershipType.Unknow ||
         customer.MembershipTypeId == MembershipType.PayasYouGo)
        return ValidationResult.Success;

      if (customer.Birthdate == null)
        return new ValidationResult("birthdate is required");

      var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

      return (age >= 18)
        ? ValidationResult.Success
        : new ValidationResult("Customer should be at least 18 years old to go on a membership.");   
    }
  }
}
