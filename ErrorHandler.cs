using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktikfall
{
    class ErrorHandler
    {

        public string HandleError(Exception ex)
        {
            string exmessage = ex.Message;

            if (exmessage.Contains("There was no endpoint listening"))
            {
                return "Error: EndpointNotFoundException. Web servern kunde inte nås.";
            }

            return "Error: Unknown exception. Någonting gick fel när web service skulle konsumeras.";

        }
    }
}
