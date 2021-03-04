using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Models
{
    public class ClientRequest
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        //public string ResourceName { get; set; }
    }
}
