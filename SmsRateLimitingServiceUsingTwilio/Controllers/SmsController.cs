using Microsoft.AspNetCore.Mvc;
using SmsRateLimitingServiceUsingTwilio.Services;
using System.Threading.Tasks;

namespace SmsRateLimitingServiceUsingTwilio.Controllers
{
    [ApiController]
    [Route("api/sms")]
    public class SmsController : Controller
    {
        private readonly RateLimiterService _rateLimiter;
        private readonly TwilioSmsService _twilioSmsService;
        public SmsController(RateLimiterService rateLimiter, TwilioSmsService twilioSmsService)
        {
            _rateLimiter = rateLimiter;
            _twilioSmsService = twilioSmsService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendSms([FromBody] SmsRequest request)
        {
            bool canSend = await _rateLimiter.CanSendMessageAsync(request.PhoneNumber);
            if (!canSend)
            {
                return BadRequest(new { Message = "Rate limit exceeded. Try again later." });
            }

            string messageSid = await _twilioSmsService.SendSmsAsync(request.PhoneNumber, request.Message);
            return Ok(new { Message = "SMS sent successfully", MessageSid = messageSid });
        }
    }

    public class SmsRequest
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
