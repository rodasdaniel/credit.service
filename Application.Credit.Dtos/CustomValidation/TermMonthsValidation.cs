using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Credit.Dtos
{
    public class TermMonthsValidation : ValidationAttribute
    {
        public TermMonthsValidation()
        {
        }
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            int intValue = Convert.ToInt32(value);
            return (intValue == 2 || intValue == 4
                || intValue == 6 || intValue == 12);
        }
    }
}
