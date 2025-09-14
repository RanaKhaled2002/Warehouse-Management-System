using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseBLL.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; }

        public AppException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
           : base(message)
        {
            StatusCode = (int)statusCode;
        }
    }
}
