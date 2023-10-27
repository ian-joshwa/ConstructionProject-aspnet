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
	public class StockUpdatesRepository : Repository<StockUpdates>, IStockUpdatesRepository
	{
		private ApplicationDbContext _context;

		public StockUpdatesRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(StockUpdates stockUpdates)
		{
			_context.StockUpdates.Update(stockUpdates);
		}
	}
}
