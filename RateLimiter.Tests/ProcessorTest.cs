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
    public class ProcessorTest
    {
        private RateLimiterProcessor processor;

        [SetUp]
        public void SetUp()
        {
            ConfigurationManager.AppSettings["PeriodSetting"] = "5s";
            ConfigurationManager.AppSettings["TimespanPassedSetting"] = "15s";
            ConfigurationManager.AppSettings["MaxRequestLimit"] = "2";
            ConfigurationManager.AppSettings["LocationPeriodSetting"] = "5h";
            ConfigurationManager.AppSettings["LocationTimespanPassedSetting"] = "15m";
            ConfigurationManager.AppSettings["LocationWithDifferentPeriodSetting"] = "";
            ConfigurationManager.AppSettings["ResourceARuleSetting"] = "1";
            ConfigurationManager.AppSettings["ResourceBRuleSetting"] = "2|3";
            ConfigurationManager.AppSettings["ResourceCRuleSetting"] = "2";

            processor = new RateLimiterProcessor();
        }


        [Test]
        public void Test_ResourceA_Only_MaxRequest_Rule_Return_True() //Max Request Within Limit Return True;
        {
            ClientIdentity identity = new ClientIdentity();
            identity.ClientSessionId = "session";
            identity.ClientToken = "client";
            identity.IsClientTokenAuthenticated = true;
            identity.IsSessionValid = true;
            identity.Resource = "resourcea";
            identity.Location = "US";
            bool firstCallResult = processor.ValidatorClientRequest(identity);

            System.Threading.Thread.Sleep(1000);
            bool secondCallResult = processor.ValidatorClientRequest(identity);
            Assert.That(secondCallResult, Is.True);
        }

        [Test]
        public void Test_ResourceA_Only_MaxRequest_Rule() //Max Request Over Limit Return False;
        {
            ClientIdentity identity = new ClientIdentity();
            identity.ClientSessionId = "session";
            identity.ClientToken = "client";
            identity.IsClientTokenAuthenticated = true;
            identity.IsSessionValid = true;
            identity.Resource = "resourcea";
            identity.Location = "US";
            bool firstCallResult = processor.ValidatorClientRequest(identity);
            bool secondCallResult = processor.ValidatorClientRequest(identity);
            bool thirdCallResult = processor.ValidatorClientRequest(identity);
            bool fourthCallResult = processor.ValidatorClientRequest(identity);
            Assert.That(thirdCallResult, Is.True);
        }



    }
}
