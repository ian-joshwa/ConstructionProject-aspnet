using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.ViewModels
{
	public class ProjectVM
	{
		public IEnumerable<Project> Projects = new List<Project>();

		public Project Project { get; set; }
	}
}
