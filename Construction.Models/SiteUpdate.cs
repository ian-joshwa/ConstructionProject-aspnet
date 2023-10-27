using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Construction.Models
{
    public class SiteUpdate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ValidateNever]
        public Project Project { get; set; }

        [ValidateNever]
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int NumberOfMasons { get; set; }

        public int NumberOfLabours { get; set; }

        public DateTime Date { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
