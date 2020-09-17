using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Recruiting.BL.Models
{
    public class Account
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public IList<string> Roles { get; set; }
    }
}
