using ExpressPaymentTest.Services.DTO;
using ExpressPaymentTest.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.ILogic
{
    public interface IUserService
    {
        Task<HttpResponse<UserDTO>> GetbyUsername(string username);
        Task<HttpResponse<UserDTO>> GetbyEmail(string email);
        Task<HttpResponse<UserDTO>> AuthenticateUser(AuthModel authModel);
        Task<HttpResponse<UserDTO>> CreateUser(RegistrationModel user);
        Task<IEnumerable<UserDTO>> GetAllUsers();
    }
}
