
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1274_RECUITE.ViewModels
{
     public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "שם משתמש:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה:")]
        public string password { get; set; }

        [Display(Name = "זכור אותי")]
        public bool rememberMe { get; set; }
        
    }
}
