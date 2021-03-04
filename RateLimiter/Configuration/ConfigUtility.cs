using System.Configuration;
using System.Collections.Generic;
using RateLimiter.Models;
using System.Linq;
using System;
using RateLimiter.Rules;
using System.Threading.Tasks;

namespace RateLimiter.Configuration
{
    /// <summary>
    /// This is the utility to read the config file.
    /// For rules:
    /// 1 - Max Request Per Timespan Rule
    /// 2 - Timespan Passed Rule
    /// 3 - End point Rule
    /// 4 - 
    /// ...
    /// </summary>
    public class ConfigUtility
    {
        #region private member config setting
        private static string Period = ConfigurationManager.AppSettings["PeriodSetting"];
        private static string PassedTimespan = ConfigurationManager.AppSettings["TimespanPassedSetting"];
        private static int MaxLimit = int.Parse(ConfigurationManager.AppSettings["MaxRequestLimit"]);

        private static string Location = ConfigurationManager.AppSettings["LocationWithDifferentPeriodSetting"];
        private static string LocationPeriod = ConfigurationManager.AppSettings["LocationPeriodSetting"];
        private static string LocationTimespanPassed = ConfigurationManager.AppSettings["LocationTimespanPassedSetting"];


        private static string RescourceARules = ConfigurationManager.AppSettings["ResourceARuleSetting"];
        private static string RescourceBRules = ConfigurationManager.AppSettings["ResourceBRuleSetting"];
        private static string RescourceCRules = ConfigurationManager.AppSettings["ResourceCRuleSetting"];
        #endregion private member config setting

        public static ConfigSetting ResourceASetting = SetUpResourceConfig();
        public static ConfigSetting ResourceBSetting = ResourceASetting;
        public static ConfigSetting ResourceCSetting = ResourceASetting;

        public static List<IClientRequestRule> GetMatchingRulesByResourceName(string resource)
        {
            return GetMatchingRulesByResource(resource);
        }

        public static ConfigSetting GetSettingByResource(string resource)
        {

            switch (resource.Trim().ToLower())
            {
                case "resourcea":
                    return ResourceASetting;
                case "resourceb":
                    return ResourceBSetting;
                case "resourcec":
                    return ResourceCSetting;
                default:
                    throw new Exception($"Resource {resource} Rules not found");
            }
        }


        private static ConfigSetting SetUpResourceConfig()
        {
            ConfigSetting config = new ConfigSetting();
            config.Period = Period;
            config.PassedTimespan = PassedTimespan;
            config.MaxRequestLimit = MaxLimit;
            config.LocationList = string.IsNullOrEmpty(Location) ? new List<string>() : Location.Split('|').ToList();
            config.LocationPeriod = LocationPeriod;
            config.LocationTimespanPassed = LocationTimespanPassed;
            return config;
        }

        private static List<IClientRequestRule> GetMatchingRulesByResource(string resource)
        {
            List<IClientRequestRule> matchingRules = new List<IClientRequestRule>();
            List<int> ruleList = new List<int>();

            switch (resource.Trim().ToLower())
            {
                case "resourcea":
                    matchingRules = GetMatchingRules(RescourceARules);
                    break;
                case "resourceb":
                    matchingRules = GetMatchingRules(RescourceBRules);
                    break;
                case "resourcec":
                    matchingRules = GetMatchingRules(RescourceCRules);
                    break;
                default:
                    throw new Exception($"Resource {resource} Rules not found");
            }
            return matchingRules;
        }

        private static List<IClientRequestRule> GetMatchingRules(string rules)
        {
            List<int> ruleList = rules.Split('|').ToList().Select(x => int.Parse(x)).ToList();
            List<IClientRequestRule> matchingRules = new List<IClientRequestRule>();
            if (ruleList.IndexOf(1) != -1)
                matchingRules.Add(new MaxRequestPerPeriodRule());
            if (ruleList.IndexOf(2) != -1)
                matchingRules.Add(new TimespanPassedRule());
            if (ruleList.IndexOf(3) != -1)
                matchingRules.Add(new EndPointRule());
            return matchingRules;
        }


    }
}
