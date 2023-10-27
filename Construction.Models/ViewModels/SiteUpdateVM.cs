using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Construction.Models.ViewModels
{
	public class SiteUpdateVM
	{
		public IEnumerable<SiteUpdate> SiteUpdates { get; set; } = Enumerable.Empty<SiteUpdate>();

		public SiteUpdate SiteUpdate { get; set; }

		public IEnumerable<SelectListItem> Projects { get; set; } = Enumerable.Empty<SelectListItem>();

	}
}
