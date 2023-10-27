using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DataAccessLayer.Data;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Construction.Models;

namespace Construction.DataAccessLayer.Infrastructure.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _context;
		public IServiceRepository Service { get; private set; }

		public IProjectRepository Project { get; private set; }

		public IApplicationUserRepository ApplicationUser { get; private set; }

		public IStockTypeRepository StockType { get; private set; }

		public IStockUpdatesRepository StockUpdate { get; private set; }

		public ITaskListRepository TaskList { get; private set; }

		public ISiteUpdateRepository SiteUpdates { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Service = new ServiceRepository(context);
			Project = new ProjectRepository(context);
			ApplicationUser = new ApplicationUserRepository(context);
			StockType = new StockTypeRepository(context);
			StockUpdate = new StockUpdatesRepository(context);
			TaskList = new TaskListRepository(context);
			SiteUpdates = new SiteUpdateRepository(context);
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
