using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BookStore.ViewModels
{
    public class EditViewModel
    {
        public EditViewModel()
        {
            Users = new List<string> ();
        }
        public string ID { get; set; }

        [Required(ErrorMessage ="Role Name is required.")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
