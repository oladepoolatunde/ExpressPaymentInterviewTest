using ExpressPaymentTest.Services.DTO;
using ExpressPaymentTest.Services.ILogic;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.Logic
{
    public class EmailServices : IEmailServices
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public EmailServices(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public Task<bool> SendEmailAsync(EmailModel emailModel)
        {

            return Task.FromResult(true);   
        }

        public Task<bool> SendReport()
        {
          return Task.FromResult(true);
        }
    }
}
