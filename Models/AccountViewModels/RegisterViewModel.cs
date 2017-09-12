using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace serverSideCapstone.Models.AccountViewModels
{
    public class RegisterViewModel
    {   
        [Display(Name = "Pups User Name")]
        public string DogName {get; set;}

        [Display(Name = "Owner First Name")]
        public string OwnerFirstName {get; set;}
        [Display(Name = "Owner Last Name")] 
        public string OwnerLastName {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set;}
    }
}
