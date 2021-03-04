using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Rules
{
    public static class Extensions
    {
        public static TimeSpan ToTimeSpan(this string timeSpan)
        {
            TimeSpan span;
            int valueLength = timeSpan.Length - 1;
            string timeSpanValue = timeSpan.Substring(0, valueLength);
            string timeSpanUnitValue = timeSpan.Substring(valueLength, 1);

            switch (timeSpanUnitValue)
            {
                case "s":
                    span = TimeSpan.FromSeconds(double.Parse(timeSpanValue));
                    break;
                case "m":
                    span = TimeSpan.FromMinutes(double.Parse(timeSpanValue));
                    break;
                case "h":
                    span = TimeSpan.FromHours(double.Parse(timeSpanValue));
                    break;
                case "d":
                    span = TimeSpan.FromDays(double.Parse(timeSpanValue));
                    break;
                default:
                    span = TimeSpan.Zero;
                    break;
            }
            return span;
        }
    }
}

