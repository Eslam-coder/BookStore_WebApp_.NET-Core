using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Utilites
{
    public class ValidEmailDomainAttribute :ValidationAttribute 
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            string [] enteredEmailDomain = value.ToString().Split('@');
           return  enteredEmailDomain[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
