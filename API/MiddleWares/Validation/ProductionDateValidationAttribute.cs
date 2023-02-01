using System.ComponentModel.DataAnnotations;

namespace WebAPI_one.Validation
{
    public class ProductionDateValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            //pattern matching 
            //safe casting(if fail, return null not exception)
            return value is DateTime date && date <= DateTime.Now;
        }
    }
}
