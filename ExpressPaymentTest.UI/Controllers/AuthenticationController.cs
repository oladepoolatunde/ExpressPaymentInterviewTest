using ExpressPaymentTest.Services.DTO;
using ExpressPaymentTest.Services.ILogic;
using ExpressPaymentTest.Services.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpressPaymentTest.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<HttpResponse<UserDTO>>> Login(AuthModel loginModel) 
        {
           
            if(ModelState.IsValid)
            {
                return View(loginModel);    
            }
            var result =  await _userService.AuthenticateUser(loginModel);
            
            if (result.status == (int) HttpStatusCode.OK)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.message;
                return RedirectToAction(nameof(Login));
            }
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<HttpResponse<UserDTO>>> Registration(RegistrationModel model)
        {
            if(ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "User";
            var result = await _userService.CreateUser(model);
                TempData["msg"] = result.message;
            return RedirectToAction(nameof(Registration));
        }


        //Endpoint to register Admin
        [AllowAnonymous]
        public async Task<IActionResult> AdminUser()
        {
            var model = new RegistrationModel
            {
                FirstName = "Express",
                LastName = "Payment",
                Email = "expresspayment@gmail.com",
                Role = "Admin@123"
            };
          var results = await _userService.CreateUser(model);
            return Ok(results);
        }
            
            









    }
}
