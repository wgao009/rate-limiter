using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Models
{
    public class ConfigSetting
    {
        /// <summary>
        /// Rate limit period setting in 5s, 10m, 2h, 1d (example)
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Rate limit timespan passed setting since last call in 5s, 10m, 2h, 1d (example)
        /// </summary>
        public string PassedTimespan { get; set; }

        /// <summary>
        /// Rate limit period setting in 5s, 10m, 2h, 1d (example)
        /// </summary>
        public string LocationPeriod { get; set; }

        /// <summary>
        /// Rate limit timespan passed setting since last call in 5s, 10m, 2h, 1d (example)
        /// </summary>
        public string LocationTimespanPassed { get; set; }


        /// <summary>
        /// Max Request Allowed per period
        /// </summary>
        public int MaxRequestLimit { get; set; }

        /// <summary>
        /// If config enabled location check rule
        /// </summary>
        public bool LocationEnabled { get; set; }

        /// <summary>
        /// List of Location that have a different timespan passed and period setting
        /// </summary>
        public List<string> LocationList { get; set; }
    }
}
