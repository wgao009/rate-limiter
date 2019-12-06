using System;
using NUnit.Framework;

namespace RateLimiter.Tests
{
    [TestFixture]
    public class RateLimiterTest
    {
        [Test]
		[TestCase(15,false)]
        [TestCase(5,true)]
		public void Overload_Fail(int calls, bool request)
        {
			var rateLimiter = new RateLimiter();
			var user = Guid.NewGuid();
			var resource = rateLimiter.KnownResource;
			var result = true;

			for (int i = 0; i < calls; i++)
			{
				result = result && rateLimiter.GetRequest(user, resource);
			}

            Assert.That(result == request);
        }
    }
}
