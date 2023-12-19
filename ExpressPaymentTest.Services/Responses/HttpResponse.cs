using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.Responses
{
    public class HttpResponse<T> where T : class
    {
        public int status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public HttpResponse(int _status, string _msg, T _responsedata)
        {
            data = _responsedata;
            status = _status;
            message = _msg;
        }
    }
}
