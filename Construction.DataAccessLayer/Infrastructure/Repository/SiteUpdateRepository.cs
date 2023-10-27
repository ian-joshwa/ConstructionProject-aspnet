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
	public class SiteUpdateRepository : Repository<SiteUpdate>, ISiteUpdateRepository
	{

		private ApplicationDbContext _context;
		public SiteUpdateRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(SiteUpdate siteUpdate)
		{
			_context.SiteUpdates.Update(siteUpdate);
		}
	}
}
