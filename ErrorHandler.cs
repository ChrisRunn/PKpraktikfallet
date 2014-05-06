using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktikfall
{
    class ErrorHandler
    {

        //Handles errors thrown from client
        public string HandleError(Exception ex)
        {
            string exmessage = ex.Message;            

            if (exmessage.Contains("There was no endpoint listening"))
            {
                return "Error: EndpointNotFoundException. Web servern kunde inte nås.";
            }
            else if (exmessage.Contains("The maximum message size quota for incoming messages (65536) has been exceeded."))
            {
                return "Error: CommunicationException. Filen är för stor för att läsas. ";

            }else if(exmessage.Contains("SqlException"))
            {
                return "Error: SqlException. Något gick fel med webtjänstens databas. ";
            }else if(ex.Message.Contains("SoapException"))
            {
                return "Error: SoapException. Något  gick fel med web service.";
            }

            return "Error: Unknown exception. Någonting gick fel när web service skulle konsumeras.";

        }
    }
}
