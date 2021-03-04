using System;
using System.Collections.Generic;
using System.Linq;
using RateLimiter.Models;
using RateLimiter.Configuration;

namespace RateLimiter.Rules
{
    /// <summary>
    /// TimespanPassedRule
    /// This will only allow reqest with config TimespanPassed
    /// if the client request location is in the config location list, we use location Timespan Passed Period
    /// </summary>
    public class TimespanPassedRule : IClientRequestRule
    {
        public virtual bool ValidateRequest(ConfigSetting configSetting, List<ClientRequest> clientRequestList, string location)
        {
            if (clientRequestList == null)
            {
                return true;
            }
            TimeSpan span = configSetting.LocationList.Contains(location.Trim().ToUpper()) ? configSetting.LocationTimespanPassed.ToTimeSpan() : configSetting.PassedTimespan.ToTimeSpan();
            var requestWithinTimespanList = clientRequestList.Where(x => (DateTime.UtcNow - x.Timestamp) < span);
            return !requestWithinTimespanList.Any();

        }


    }


}
