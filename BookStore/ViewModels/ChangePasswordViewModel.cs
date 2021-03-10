using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BookStore.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="New Password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name ="Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare(" NewPassword", ErrorMessage = "The New Passowrd and Confirm Password don't match. ")]
        public string ConfirmPassword { get; set; }
    }
}
