using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class Register
    {

        [Required(ErrorMessage = "Please provide DOB",AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public string DateofBirth { get; set; }

        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Please provide firstname")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Please provide lastname")]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage="Please enter Email Id")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Please provide phone")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }


    }
}