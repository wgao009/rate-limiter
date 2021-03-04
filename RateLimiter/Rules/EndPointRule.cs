using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiter.Models;

namespace RateLimiter.Rules
{
    public class EndPointRule : IClientRequestRule
    {

        /// <summary>
        /// Fake Rule to demonstrate we can add more rule
        /// </summary>
        /// <param name="configSetting"></param>
        /// <param name="clientRequestList"></param>
        /// <returns></returns>
        public virtual bool ValidateRequest(ConfigSetting configSetting, List<ClientRequest> clientRequestList, string location)
        {
            return true;

        }
    }
}
