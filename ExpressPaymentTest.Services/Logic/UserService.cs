using AutoMapper;
using ExpressPaymentTest.Domain.Entities;
using ExpressPaymentTest.Services.DTO;
using ExpressPaymentTest.Services.ILogic;
using ExpressPaymentTest.Services.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.Logic
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;
        private readonly IMapper _autoMapper;
        public UserService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<User> roleManager, IMapper autoMapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _autoMapper = autoMapper;
        }

        public async Task<HttpResponse<UserDTO>> AuthenticateUser(AuthModel authModel)
        {
            try 
             { 
               string message; int statusCode;
                var user = await _userManager.FindByEmailAsync(authModel.Email);
                if (user == null)
                {
                    message = "Username or Password not correct";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    return new HttpResponse<UserDTO> (statusCode, message, null );
                }
                else 
                {
                    if (!await _userManager.CheckPasswordAsync(user, authModel.Password))
                    {
                        message = "Invalid Username or Password";
                        statusCode = (int)HttpStatusCode.BadRequest;
                        return new HttpResponse<UserDTO>(statusCode, message, null);
                    }

                    else
                    {
                        var role = await _roleManager.GetRoleNameAsync(user);
                        var authClaims = new List<Claim>
                        {
                           new Claim(ClaimTypes.Name, user.Email),
                        };

                        foreach (var userRole in role)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, role));
                        }
                            message = "Login successful";
                            statusCode = (int)HttpStatusCode.OK;
                        var userdto = new UserDTO {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            UserName= user.UserName,
                            Role = role   
                        };
                           
                        return new HttpResponse<UserDTO> (statusCode, message, userdto); 
                    }
                
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
         
        }

        public async Task<HttpResponse<UserDTO>> CreateUser(RegistrationModel user)
        {
            try
            {
                string message; int statusCode;
                var existingUser = this.GetbyEmail(user.Email);
                if (existingUser != null)
                {
                    message = "";
                    statusCode =(int)HttpStatusCode.BadRequest;
                    return new HttpResponse<UserDTO>(statusCode,message, null);
                }

                var newUser = _autoMapper.Map<User>(user);  

                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!await _roleManager.RoleExistsAsync(user.Role))
                {
                    message = "Failed to create or update role";
                    statusCode =(int)HttpStatusCode.BadRequest;
                    return new HttpResponse<UserDTO>(statusCode, message, null);
                }

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, user.Role);
                    message = "User Created Successfully";
                    statusCode = (int)HttpStatusCode.OK;
                   var createdUser = _autoMapper.Map<UserDTO>(newUser);
                    return new HttpResponse<UserDTO>(statusCode, message, createdUser);
                }
                else
                {
                    message = "Failed to create or update role";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    return new HttpResponse<UserDTO>(statusCode, message, null);
                }
            }
            catch (Exception ex)
            {

                throw ex;
               
            }
        }
        public async Task<HttpResponse<UserDTO>> GetbyEmail(string email)
        {
            try
            {
                string message; int statusCode;
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    message = "User not found";
                    statusCode = (int)HttpStatusCode.NotFound;
                    return new HttpResponse<UserDTO>(statusCode, message, null);
                }
                else
                {
                    message = "User fetched Successfully";
                    statusCode = (int)HttpStatusCode.OK;
                    var fetchedUser = _autoMapper.Map<UserDTO>(user);
                    return new HttpResponse<UserDTO>(statusCode, message, fetchedUser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponse<UserDTO>> GetbyUsername(string username)
        {
            try
            {
                string message; int statusCode;
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    message = "User not found";
                    statusCode = (int)HttpStatusCode.NotFound;
                    return new HttpResponse<UserDTO>(statusCode, message, null);
                }
                else
                {
                    message = "User fetched Successfully";
                    statusCode = (int)HttpStatusCode.OK;
                    var fetchedUser = _autoMapper.Map<UserDTO>(user);
                    return new HttpResponse<UserDTO>(statusCode, message, fetchedUser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
               
                var users = await _userManager.Users.ToListAsync();
                if(users != null)
                {
                   var returnedUsers =  _autoMapper.Map<List<UserDTO>>(users);
                    return returnedUsers;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
