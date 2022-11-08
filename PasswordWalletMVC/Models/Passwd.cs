using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordWalletMVC.Models
{
    public class Passwd
    {
        [Key]
        public int PasswdId { get; set; }

        [Required(ErrorMessage = "UserName required")]
        //[ForeignKey("UserAccount")]
        public int UserNameId { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        [Required(ErrorMessage = "UserName required")]
        public int PasswdName { get; set; }


        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }


    }
}
