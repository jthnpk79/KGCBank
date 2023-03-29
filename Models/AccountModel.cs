using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace KGCBank.Models
{
    public class AccountModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Account Number")]
        public string AccNumber { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Account Type")]
        public string AccType { get; set; }

        [Display(Name = "Branch")]
        public string BranchId { get; set; }

        [Display(Name = "RegistrationDate")]
        public string RegDate { get; set; }

        [Display(Name = "Balance")]
        public string Balance { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Picture")]
        public string File { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}