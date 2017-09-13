using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        public List<IFormFile> Image {get; set;} = new List<IFormFile>();
        public string ImgPath {get; set;}
        public IndexViewModel()
        {
            ApplicationUser = new ApplicationUser();
        }
        public ApplicationUser ApplicationUser {get; set;}
        public string StatusMessage { get; set; }
    }
}
