using ExpressPaymentTest.Domain.Entities;
using ExpressPaymentTest.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.ILogic
{
    public interface IGenderServices
    {
        Task<HttpResponse<Gender>> GetbyId(int id);
        Task<HttpResponse<IEnumerable<Gender>>> GetAll(int id);
    }
}
