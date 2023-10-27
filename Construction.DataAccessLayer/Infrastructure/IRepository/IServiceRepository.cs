using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.Models;

namespace Construction.DataAccessLayer.Infrastructure.IRepository
{
	public interface IServiceRepository : IRepository<Service>
	{

		void Update(Service serrvice);

		IEnumerable<Service> FeaturedServices();

	}
}
