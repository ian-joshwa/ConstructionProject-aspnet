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
	public class ServiceRepository : Repository<Service>, IServiceRepository
	{
		private ApplicationDbContext _context;

		public ServiceRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public IEnumerable<Service> FeaturedServices()
		{
			return _context.Services.Where(x => x.IsFeatured);
		}

		public void Update(Service service)
		{
			_context.Services.Update(service);
		}
	}
}
