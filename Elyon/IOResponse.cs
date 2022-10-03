using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon
{
    /// <summary>
    /// IOResponse is meant to be used internally by Elyon, once the "Data Event" is called, the data is then stored 
    /// into this object and provided methods are used to correctly format the received data. 
    /// Methods are provided for this correct formatting, the current method is standard OBD response formatting and 
    /// another can be used for CAN bus responses.
    /// </summary>
    public class IOResponse
    {

        public string PID;
        public decimal Value;
        private IOCommand Command;
        public string RawResponse;
        public bool Status = false;
        public string ResponseCode;
        public string ErrorMessage;

        public IOResponse(IOCommand command, string response)
        {
            Command = command;
            FormatResponse(response);
        }

        public void FormatResponse(string response)
        {
            // Remove Characters
            RawResponse = response.Remove(response.Remove(response.Length - 1).Length - 1);

            // Doesn't remove \n character
            //RawResponse = response.Remove(response.Remove(response.Length - 1).Length - 1);

            // Split values
            string[] splittedRes = RawResponse.Split(' ');

            // Convert to Decimal
            //ResponseCode = splittedRes[0];
            ResponseCode = "";

            if (!ResponseCode.Equals(Command.ExpectedResponseMode))
            {
                Status = false;
                ErrorMessage = "Response Mode is not Congruent";
            }

            if (splittedRes.Length > 1)
            {
                PID = splittedRes[1];
                Value = Int16.Parse(splittedRes[2] + splittedRes[3], System.Globalization.NumberStyles.HexNumber);
            }else
            {
                PID = "";
                Value = 0;
            }
        }
    }
}
