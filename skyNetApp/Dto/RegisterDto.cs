using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Dto
{
    public class RegisterDto
    {

        [Required]
        public string  DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //regular expression for strong password from regexlib website
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",ErrorMessage ="password must have one upper case one lowe case one numbric and one special chs and and at least 6 chs)")]
        public string Password { get; set; }

    }
}
