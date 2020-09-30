using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Recruiting.BL.Models
{
    public class Account : IEquatable<Account>
    {
        public static readonly Account _EmptyAccount = new Account { UserId = String.Empty };

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


        public static bool IsEmpty(Account account)
            => account == _EmptyAccount;
        public override bool Equals(object obj)
        {
            return Equals(obj as Account);
        }

        public bool Equals(Account other)
        {
            return other != null &&
                   UserId == other.UserId &&
                   FullName == other.FullName &&
                   Email == other.Email &&
                   BirthDate == other.BirthDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, FullName, Email, BirthDate, Roles);
        }

        public static bool operator ==(Account left, Account right)
        {
            return EqualityComparer<Account>.Default.Equals(left, right);
        }

        public static bool operator !=(Account left, Account right)
        {
            return !(left == right);
        }
    }
}
