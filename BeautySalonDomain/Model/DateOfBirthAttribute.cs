using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BeautySalonDomain.Model;

public class DateOfBirthAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateOnly? date = value as DateOnly?;
        if (date != null)
        {
            if (date > DateOnly.FromDateTime(DateTime.Today))
            {
                ErrorMessage = "Перевірте дату Вашого дня народження";
                return false;
            }
        }
        return true;
    }
}

