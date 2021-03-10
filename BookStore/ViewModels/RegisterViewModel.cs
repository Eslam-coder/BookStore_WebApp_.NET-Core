using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using BookStore.Utilites;

namespace BookStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Enter email please.")]
        [Remote("IsEmailInUse", controller:"Account")]
        //Custom Validation 
        [ValidEmailDomain(allowedDomain:"google.com",ErrorMessage ="Email Domain must be google.com")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password,ErrorMessage ="enter password please.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password",ErrorMessage ="password and confirmation don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
