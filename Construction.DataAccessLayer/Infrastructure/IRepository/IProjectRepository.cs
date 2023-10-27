using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.Models;

namespace Construction.DataAccessLayer.Infrastructure.IRepository
{
	public interface IProjectRepository : IRepository<Project>
	{


		void Update(Project project);

		IEnumerable<Project> GetFeatured();

	}
}
