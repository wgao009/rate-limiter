using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiter.Models;
using RateLimiter.Configuration;

namespace RateLimiter
{
    /// <summary>
    /// RateLimiterProcessor
    /// Assumption - client authentication and session checking is done in some other module
    /// 
    /// This Rate Limiter Processor will process the client request based on configration setting
    /// All the rule settings are configrable and rules for each resource are also configurable based on situation
    /// 
    /// TO_DO : To Improve. The dictionary will grow. In reality will need lock and remove items from the dictionary when timespan passed
    /// </summary>
    public class RateLimiterProcessor
    {
        private static readonly Dictionary<string, List<ClientRequest>> resourceAClientRequests = new Dictionary<string, List<ClientRequest>>();
        private static readonly Dictionary<string, List<ClientRequest>> resourceBClientRequests = new Dictionary<string, List<ClientRequest>>();
        private static readonly Dictionary<string, List<ClientRequest>> resourceCClientRequests = new Dictionary<string, List<ClientRequest>>();

        public bool ValidatorClientRequest(ClientIdentity identity)
        {
            if (!identity.IsClientTokenAuthenticated || !identity.IsSessionValid)
            {
                return false;
            }
            bool valid = true;

            switch(identity.Resource.Trim().ToLower())
            {
                case "resourcea":
                    ProcessRules(resourceAClientRequests, ConfigUtility.ResourceASetting, identity.ClientToken, identity.Resource, identity.Location);
                    break;
                case "resourceb":
                    ProcessRules(resourceBClientRequests, ConfigUtility.ResourceBSetting, identity.ClientToken, identity.Resource, identity.Location);
                    break;
                case "resourcec":
                    ProcessRules(resourceCClientRequests, ConfigUtility.ResourceCSetting, identity.ClientToken, identity.Resource, identity.Location);
                    break;
                default:
                    throw new Exception($"Resource {identity.Resource} Rules not found");
            }
            return valid;
        }

        private bool ProcessRules(Dictionary<string, List<ClientRequest>> dictionary, ConfigSetting setting, string token, string resource, string location)
        {
            ClientRequest request = new ClientRequest();
            bool valid = true;
            List<ClientRequest> clientRequests = GeClientRequestsByToken(token, dictionary);
            var clientRequestedResourceRules = ConfigUtility.GetMatchingRulesByResourceName(resource);
            clientRequestedResourceRules.ForEach(r =>
            {
                valid = r.ValidateRequest(setting, clientRequests, location);
            });
            if (dictionary.ContainsKey(token))
            {
                dictionary[token].Add(request);
            }
            else
            {
                dictionary.Add(token, new List<ClientRequest> { request });
            }
            return valid;
        }

        private List<ClientRequest> GeClientRequestsByToken(string token, Dictionary<string, List<ClientRequest>> clientRequests)
        {
            clientRequests.TryGetValue(token, out List<ClientRequest> passedRequestList);
            return passedRequestList;
        }








    }
}
