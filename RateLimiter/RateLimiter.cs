using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	public class DummyAPI
	{
		private readonly Dictionary<Guid, IRateLimiter> limiterCorrespondance;
		private readonly Guid knownResource1 = Guid.NewGuid();

		public DummyAPI(IRateLimiter limiter)
		{
			limiterCorrespondance = new Dictionary<Guid, IRateLimiter>();
			limiterCorrespondance.Add(knownResource1, limiter);//new RateLimiterCount(10,TimeSpan.FromMilliseconds(1000))
		}

		public Guid KnownResource
		{
			get { return knownResource1; }
		}

		public bool GetRequest(Guid userId, Guid resourceId)
		{
			var limiter = limiterCorrespondance[resourceId];
			return limiter.Check(userId);
		}
	}

	public interface IRateLimiter
	{
		bool Check(Guid userId);
	}

	internal class RateLimiterCount : IRateLimiter
	{
		internal RateLimiterCount(uint requestLimit, TimeSpan timeLimit)
		{
			this.requestLimit = requestLimit;
			this.timeLimit = timeLimit;
			usersInfo = new Dictionary<Guid, RequestInfo>();
		}

		private readonly uint requestLimit;
		private readonly TimeSpan timeLimit;
		private readonly Dictionary<Guid, RequestInfo> usersInfo;
		
		internal struct RequestInfo
		{
			internal DateTime FirsRequestTime { get; set; }
			internal uint RequestCount { get; set; }
		}

		public bool Check(Guid userId)
		{
			if (!usersInfo.ContainsKey(userId))
			{
				usersInfo.Add(userId, new RequestInfo());
			}
			var info = usersInfo[userId];

			var currentTime = DateTime.Now;
			if (timeLimit > currentTime - info.FirsRequestTime)
			{
				info.FirsRequestTime = currentTime;
				info.RequestCount = 0;
			}
			if (requestLimit > info.RequestCount)
			{
				info.RequestCount++;
				return true;
			}
			return false;
		}
	}
}
