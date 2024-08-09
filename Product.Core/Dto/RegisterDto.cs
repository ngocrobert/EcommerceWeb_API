using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5, ErrorMessage ="Min length 5 characters")]
        public string DisplayName { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "It expects atleast 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 6-10 characters. " + "Match all alphanumeric character and predefined wild characters. "+"Password must consists of at least 8 characters and not more than 15 characters.")]
        public string Password { get; set; }

    }
}
