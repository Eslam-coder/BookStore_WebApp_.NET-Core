using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace BookStore.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter email please.")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "enter password please.")]
        public string Password { get; set; }

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl  { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
