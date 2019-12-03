using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	public class UserRequestStatistic
	{
		public DateTime LastRequestTime { get; set; }
		public string ResoursePath { get; set; }
	}
}
