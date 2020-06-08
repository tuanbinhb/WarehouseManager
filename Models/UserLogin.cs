using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace jQueryAjaxInAsp.NETMVC.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "* This field is required.")]
        [Display(Name = "Login ID")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "* This field is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PassWord { get; set; }

        public bool RememberMe { get; set; }

        public int Role { get; set; }
    }
}