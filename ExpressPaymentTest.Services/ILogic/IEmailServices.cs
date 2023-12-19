using ExpressPaymentTest.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.ILogic
{
    public interface IEmailServices
    {
        Task<bool> SendEmailAsync(EmailModel emailModel);
        Task<bool> SendReport();
    }
}
