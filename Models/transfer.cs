using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KGCBank.Models
{
    public class transfer
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "IFSC")]
        public string IFSC { get; set; }

        [Display(Name = "Account Holder")]
        public string AccountHolder { get; set; }

        [Display(Name = "Amount")]
        public float Amount { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}