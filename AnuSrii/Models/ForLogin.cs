using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnuSrii.Models
{
    public class ForLogin
    {
        [Required]
        [RegularExpression(".+@.+\\..+",ErrorMessage ="Email Formate is Wrong.")]
        [DisplayName("UserName")]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}