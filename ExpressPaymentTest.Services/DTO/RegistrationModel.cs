using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.DTO
{
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$",
      ErrorMessage = "Password must contain at least one letter, one digit, and may contain special characters.")]
        public string Password { get; set; }
        public int GenderId { get; set; }
        public string Role { get; set;}
    }
}
