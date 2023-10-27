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
    public class TaskListRepository : Repository<TaskList>, ITaskListRepository
    {
        private ApplicationDbContext _context;

        public TaskListRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(TaskList taskList)
        {
            _context.TaskList.Update(taskList);
        }
    }
}
