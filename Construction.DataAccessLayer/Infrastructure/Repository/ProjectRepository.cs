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
	public class ProjectRepository : Repository<Project>, IProjectRepository
	{
		private ApplicationDbContext _context;
		public ProjectRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

        public IEnumerable<Project> GetFeatured()
        {
			return _context.Projects.Where(x => x.IsFeatured);
        }

        public void Update(Project project)
		{
			_context.Projects.Update(project);
		}
	}
}
