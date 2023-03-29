using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace KGCBank.Models
{
    public class LoginModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name ="Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}