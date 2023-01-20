using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Common
{
    public class CheckDateAttribute : ValidationAttribute
    {
        public CheckDateAttribute()
        {

        }
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                var dt = (DateTime)value;

                if (dt >= DateTime.Now && dt.Year <= DateTime.Now.Year + 10)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
