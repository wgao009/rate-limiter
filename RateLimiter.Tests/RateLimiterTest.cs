using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using NUnit.Framework;
using RateLimiter.Configuration;
using RateLimiter.Rules;
using RateLimiter.Models;

namespace RateLimiter.Tests
{
    [TestFixture]
    public class RateLimiterTest
    {
        private MaxRequestPerPeriodRule maxRequestRule;
        private TimespanPassedRule timespanPassedRule;

        [SetUp]
        public void SetUp()
        {
            ConfigurationManager.AppSettings["PeriodSetting"] = "5s";
            ConfigurationManager.AppSettings["TimespanPassedSetting"] = "2s";
            ConfigurationManager.AppSettings["MaxRequestLimit"] = "2";
            ConfigurationManager.AppSettings["LocationPeriodSetting"] = "5h";
            ConfigurationManager.AppSettings["LocationTimespanPassedSetting"] = "15m";
            ConfigurationManager.AppSettings["LocationWithDifferentPeriodSetting"] = "";
            ConfigurationManager.AppSettings["ResourceARuleSetting"] = "1";
            ConfigurationManager.AppSettings["ResourceBRuleSetting"] = "2|3";
            ConfigurationManager.AppSettings["ResourceCRuleSetting"] = "2";

            maxRequestRule = new MaxRequestPerPeriodRule();
            timespanPassedRule = new TimespanPassedRule();
        }

        [Test]
        public void ConfigUtilityLocationEmptyStringValidation()
        {
            var result = ConfigUtility.ResourceASetting.LocationList;

            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void Check_MaxRequest_NullLibrary_ReturnTrue()
        {
            var result = maxRequestRule.ValidateRequest(ConfigUtility.ResourceASetting, null, "US");
            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_MaxRequest_EmptyLibrary_ReturnTrue()
        {
            List<ClientRequest> library = new List<ClientRequest>();
            var result = maxRequestRule.ValidateRequest(ConfigUtility.ResourceBSetting, library, "US");
            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_MaxRequest_Within_Limit_Return_True()
        {
            List<ClientRequest> library = new List<ClientRequest>()
            {
                new ClientRequest{ Timestamp = DateTime.UtcNow.AddMinutes(-60) }
            };
            var result = maxRequestRule.ValidateRequest(ConfigUtility.ResourceASetting, library, "US");
            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_MaxRequest_Over_Limit_Return_False()
        {
            List<ClientRequest> library = new List<ClientRequest>()
            {
                new ClientRequest{ Timestamp = DateTime.UtcNow.AddSeconds(-4) }
            };
            library.Add(new ClientRequest { Timestamp = DateTime.UtcNow.AddSeconds(-1) });
            var result = maxRequestRule.ValidateRequest(ConfigUtility.ResourceASetting, library, "US");
            Assert.That(result, Is.False);
        }

        [Test]
        public void Check_TimespanPassed_NullLibrary_ReturnTrue()
        {
            var result = timespanPassedRule.ValidateRequest(ConfigUtility.ResourceCSetting, null, "US");
            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_TimespanPassed_EmptyLibrary_ReturnTrue()
        {
            List<ClientRequest> library = new List<ClientRequest>();
            var result = timespanPassedRule.ValidateRequest(ConfigUtility.ResourceASetting, library, "US");
            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_TimespanPassed_Within_ReturnFalse()
        {
            List<ClientRequest> library = new List<ClientRequest>()
            {
                new ClientRequest{ Timestamp = DateTime.UtcNow.AddSeconds(-1) }
            };
            var result = timespanPassedRule.ValidateRequest(ConfigUtility.ResourceASetting, library, "US");
            Assert.That(result, Is.False);
        }

        [Test]
        public void Check_TimespanPassed_Outofspan_ReturnTrue()
        {
            List<ClientRequest> library = new List<ClientRequest>()
            {
                new ClientRequest{ Timestamp = DateTime.UtcNow.AddMinutes(-5) }
            };
            var result = timespanPassedRule.ValidateRequest(ConfigUtility.ResourceASetting, library, "US");
            Assert.That(result, Is.True);
        }


    }

}
