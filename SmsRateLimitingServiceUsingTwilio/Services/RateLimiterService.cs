using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace SmsRateLimitingServiceUsingTwilio.Services
{
    public class RateLimiterService
    {
        private readonly IDatabase _redisDb;
        private readonly int _maxPerNumber;
        private readonly int _maxGlobal;

        public RateLimiterService(IConnectionMultiplexer redis, IConfiguration config)
        {
            _redisDb = redis.GetDatabase();
            _maxPerNumber = config.GetValue<int>("RateLimits:PerNumber");
            _maxGlobal = config.GetValue<int>("RateLimits:Global");
        }

        public async Task<bool> CanSendMessageAsync(string phoneNumber)
        {
            string perNumberKey = $"sms_rate:{phoneNumber}";
            string globalKey = "sms_rate:global";

            var perNumberCount = await _redisDb.StringIncrementAsync(perNumberKey);
            var globalCount = await _redisDb.StringIncrementAsync(globalKey);

            if (perNumberCount > _maxPerNumber || globalCount > _maxGlobal)
            {
                return false;
            }

            await _redisDb.KeyExpireAsync(perNumberKey, TimeSpan.FromSeconds(1));
            await _redisDb.KeyExpireAsync(globalKey, TimeSpan.FromSeconds(1));

            return true;
        }
    }
}
