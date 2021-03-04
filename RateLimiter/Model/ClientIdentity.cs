using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Models
{
    /// <summary>
    /// Client Identity Model : can store the client token, endpoint, Httpverb, client IP address, etc.
    /// </summary>
    public class ClientIdentity
    {
        public string ClientToken { get; set; }
        public string Location { get; set; }
        public string Resource { get; set; }
        public string ClientSessionId { get; set; }
        public bool IsSessionValid { get; set; }
        public bool IsClientTokenAuthenticated { get; set; }


    }
}
