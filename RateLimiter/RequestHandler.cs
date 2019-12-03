using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	class RequestHandler
	{		
		IRepository<UserRequestStatistic> Repo { get; set; }
		IRequestLimitRule [] Rusel { get; set; }
 
		public bool CanExecute(Guid userId, IResourse resourse)
		{
			var userStatistic = Repo.Get(userId);
			if (userStatistic == null)
			{
				UpdateRequser(userStatistic);

			}
			foreach (fo)
		}
		void UpdateRequser(UserRequestStatistic statistic)
		{

			statistic.LastRequestTime = DateTime.Now;
			Repo.InsertOrUpdate(statistic);
		}
	}
}
