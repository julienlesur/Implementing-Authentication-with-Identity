using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recruiting.Data.Data
{
    // Add profile data for application users by adding properties to the RecruitingUser class
    public class RecruitingUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        public DateTime Birthdate { get; set; }
    }
}
