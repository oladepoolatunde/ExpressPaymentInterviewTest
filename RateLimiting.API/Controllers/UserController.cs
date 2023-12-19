using ExpressPaymentTest.Data.IRepository;
using ExpressPaymentTest.Services.DTO;
using ExpressPaymentTest.Services.ILogic;
using ExpressPaymentTest.Services.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimiting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
      

        public UserController(IUserService service)
        {
            _service = service;
            
        }
        [HttpGet]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
           if(ModelState.IsValid)
            {
                var result = await _service.GetAllUsers();
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
