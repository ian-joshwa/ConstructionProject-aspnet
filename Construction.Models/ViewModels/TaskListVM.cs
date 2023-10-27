using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Construction.Models.ViewModels
{
    public class TaskListVM
    {

        IEnumerable<TaskList> TaskLists { get; set; } = Enumerable.Empty<TaskList>();

        public TaskList TaskList { get; set; }

        public IEnumerable<SelectListItem> Projects { get; set; } = Enumerable.Empty<SelectListItem>();

        public IEnumerable<SelectListItem> Status { get; set; } = Enumerable.Empty<SelectListItem>();

    }
}
