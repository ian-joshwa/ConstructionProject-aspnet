using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DataAccessLayer.Infrastructure.IRepository
{
	public interface IUnitOfWork
	{
		IServiceRepository Service { get; }

		IProjectRepository Project { get; }

		IApplicationUserRepository ApplicationUser { get; }

		IStockTypeRepository StockType { get; }

		IStockUpdatesRepository StockUpdate { get; }

		ITaskListRepository TaskList { get; }

		ISiteUpdateRepository SiteUpdates { get; }

		void Save();
	}
}
