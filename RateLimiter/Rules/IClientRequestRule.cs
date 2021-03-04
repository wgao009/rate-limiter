using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiter.Models;

namespace RateLimiter.Rules
{
    public interface IClientRequestRule
    {
        bool ValidateRequest(ConfigSetting configSetting, List<ClientRequest> clientRequestList, string location);
    }
}
