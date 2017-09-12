using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace serverSideCapstone.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

         public string Address {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public int? ZipCode {get; set;}
        public string ProfileDesctiption {get; set;}

        public bool? IsIntact {get; set;}
        public int? ActivityLevelId {get; set;}
        public ActivityLevel ActivityLevel {get; set;}

        public string ImgPath {get; set;}

        public string StatusMessage { get; set; }
    }
}
