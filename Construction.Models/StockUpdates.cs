using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Construction.Models
{
	public class StockUpdates
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ProjectId { get; set; }

		[ValidateNever]
		public Project Project { get; set; }


		public DateTime Date { get; set; }

		[Required]
		public string StockType { get; set; }

		public double Old { get; set; }

		public double New { get; set; }

		public double Used { get; set; }

		public double Remaining { get; set; }
	}
}
