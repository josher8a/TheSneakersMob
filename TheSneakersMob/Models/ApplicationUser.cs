using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSneakersMob.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public void EditProfileInfo(string firstName, string lastName, string email, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Country = country;
        }
    }
}
