using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	class LastRequestRuleHandler : IRequestLimitRule
	{
		TimeSpan MinRequsetInterval { get; set; }
		public bool Allow(IResourse resourse, UserRequestStatistic statistic)
		{
			return DateTime.Now - statistic.LastRequestTime < MinRequsetInterval;
			 
		}
	}
}
