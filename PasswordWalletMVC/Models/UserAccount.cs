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

        [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual List<Passwd> UserPasswords { get; set; }

    }
}
