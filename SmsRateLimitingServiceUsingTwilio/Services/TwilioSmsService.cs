using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SmsRateLimitingServiceUsingTwilio.Services
{
    public class TwilioSmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public TwilioSmsService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _fromNumber = configuration["Twilio:FromNumber"];

            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task<string> SendSmsAsync(string toNumber, string message)
        {
            var messageResource = await MessageResource.CreateAsync(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(_fromNumber),
                body: message
            );

            return messageResource.Sid;
        }
    }
}
