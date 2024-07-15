using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.Response
{
    public class ControllerResponse
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        public ControllerResponse(bool success, object data, string message)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        public static ControllerResponse SuccessResponse(object data, string message)
        {
            return new ControllerResponse(true, data, message);
        }

        public static ControllerResponse FailureResponse(string message)
        {
            return new ControllerResponse(false, null, message);
        }
    }
}
