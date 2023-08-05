using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Attributes
{
    public class NotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }
            return false;
        }
    }
}