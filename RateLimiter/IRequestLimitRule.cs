namespace RateLimiter
{
	internal interface IRequestLimitRule
	{
		bool Allow(IResourse resourse, UserRequestStatistic statistic);
		 
	}
}