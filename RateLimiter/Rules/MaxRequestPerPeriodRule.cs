using System;
using System.Collections.Generic;
using System.Linq;
using RateLimiter.Models;
using RateLimiter.Configuration;

namespace RateLimiter.Rules
{
    /// <summary>
    /// MaxRequestPerPeriodRule
    /// This will implement Max request per time period
    /// if the client request location is in the config location list, we use location Period
    /// </summary>
    public class MaxRequestPerPeriodRule : IClientRequestRule
    {

        public virtual bool ValidateRequest(ConfigSetting configSetting, List<ClientRequest> clientRequestList, string location)
        {
            if (clientRequestList == null)
            {
                return true;
            }
            TimeSpan span = configSetting.LocationList.Contains(location.Trim().ToUpper()) ? configSetting.LocationPeriod.ToTimeSpan() : configSetting.Period.ToTimeSpan();

            var requestWithPeriodList = clientRequestList.Where(x => (DateTime.UtcNow - x.Timestamp) <= span);
            return requestWithPeriodList.Count() < configSetting.MaxRequestLimit;

        }

    }
}
