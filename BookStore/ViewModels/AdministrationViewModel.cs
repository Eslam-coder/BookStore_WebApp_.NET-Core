using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BookStore.ViewModels
{
    public class AdministrationViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
