using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.CommonHelper.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Construction.Models.ViewModels
{
	public class StockTypeVM
	{
		public StockType StockType { get; set; }

		public IEnumerable<SelectListItem> StockTypeUnits { get; set; } = Enumerable.Empty<SelectListItem>();
	}
}
