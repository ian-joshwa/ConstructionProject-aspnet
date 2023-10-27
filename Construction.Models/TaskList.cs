using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Construction.Models
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        [ValidateNever]
        public Project Project { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
