using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationsExample.CustomValidators;

namespace ModelValidationsExample.Models

{
    public class Person : IValidatableObject
    {
        [Required(ErrorMessage = "{0} is not valid")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} characters must be between {2} and {1} long")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} characters must be an alphabet, space or dots")] // allows specified characters // "*" allows multiple characters
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "{0} is not valid")]
        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        public string? email { get; set; }

        [Phone(ErrorMessage = "{0) should be a proper phone number")]
        [ValidateNever]
        public string? phone { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Compare("Password",ErrorMessage = "{0} and {1} does not match.")]
        [Display(Name = "Re-enter password")]
        public string? ConfirmPassword { get; set;}

        [Range(0,999.99, ErrorMessage = "{0} should be bewtween ${1} and ${2}")] 
        public double? Price { get; set; }
        //[MinimumYearValidator(2005, ErrorMessage = "Date of birth should be newer than {0}")]
        [MinimumYearValidator(2005)]
        [BindNever]
        public DateTime? DateOfBirth { get; set; }

        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To date'")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }
        public override string ToString()
        {
            return $"Person object - Persone name: {PersonName}, email: {email}, phone: {phone}, password: {Password}, confirm password: {ConfirmPassword}, price: {Price}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth.HasValue == false && Age.HasValue == false)
            {
                yield return new ValidationResult("Either of Date of Birth or Age must be supplied", new[] {nameof(Age)}); // yield return multiple values
            }

            
        }
    }
}
