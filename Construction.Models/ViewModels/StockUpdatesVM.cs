using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Construction.Models.ViewModels
{
	public class StockUpdatesVM
	{

		public IEnumerable<StockUpdates> StockUpdates { get; set; } = new List<StockUpdates>();

		public StockUpdates StockUpdate { get; set; }

		public IEnumerable<SelectListItem> Projects { get; set; } =	Enumerable.Empty<SelectListItem>();

		public IEnumerable<SelectListItem> StockType { get; set; } = new List<SelectListItem>();

	}
}
