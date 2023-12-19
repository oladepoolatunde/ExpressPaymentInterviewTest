using ExpressPaymentTest.Services.DTO;
using ExpressPaymentTest.Services.ILogic;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BackgroundJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ILogger _logger;
        private readonly IEmailServices _emailServices;
        private readonly IRecurringJobManager _recurringJobManager;

        public HangFireController(IBackgroundJobClient backgroundJobClient, ILogger logger, IEmailServices emailServices, IRecurringJobManager recurringJobManager)
        {
            _backgroundJobClient = backgroundJobClient;
            _logger = logger;
            _emailServices = emailServices;
            _recurringJobManager = recurringJobManager;
        }
        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(EmailModel emailModel)
        {
            try
            {
              _backgroundJobClient.Enqueue(() => _emailServices.SendEmailAsync(emailModel));
                return Ok();
            }
            
            catch (Exception ex)
            {
                _logger.LogError($"EmailService => SendEmailConfirmation => {ex}",
                    new { Controller = "EmailService", Method = "SendEmailConfirmation" });
                return BadRequest(ex);
            }
        }
        [HttpPost ]
        [Route("SendReport")]
        public async Task<IActionResult> SendReport()
        {
            try
            {
                _recurringJobManager.AddOrUpdate("jobId1", () => _emailServices.SendReport(), Cron.Daily);
                return Ok();
            }

            catch (Exception ex)
            {
                _logger.LogError($"EmailService => SendEmailConfirmation => {ex}",
                    new { Controller = "EmailService", Method = "SendReport" });
                return BadRequest(ex);
            }

        }
    }
}
