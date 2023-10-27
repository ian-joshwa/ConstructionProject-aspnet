using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.ViewModels
{
	public class ServiceVM
	{
		public IEnumerable<Service> Services { get; set; } = Enumerable.Empty<Service>();

		public Service Service { get; set; } 
	}
}
