using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace serverSideCapstone.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string DogName {get; set;}        
        [Required]
        public string OwnerFirstName {get; set;}

        [Required]
        public string OwnerLastName {get; set;}
        
        public string Address {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public int? ZipCode {get; set;}
        public string ProfileDescription {get; set;}

        public bool? IsIntact {get; set;}
        public int? ActivityLevelId {get; set;}
        public ActivityLevel ActivityLevel {get; set;}

        public string ImgPath {get; set;}

    }
}
