using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	public interface IRepository<T>
	{
		T Get(Guid id);
		T InsertOrUpdate(T t);
	}
}
