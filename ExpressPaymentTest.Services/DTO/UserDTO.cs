using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string UserName { get; set; }
        public string GenderName { get; set; }
        public string Role {  get; set; }
    }
}
