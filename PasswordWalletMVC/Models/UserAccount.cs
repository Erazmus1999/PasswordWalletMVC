using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PasswordWalletMVC.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="First Name required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Please confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
