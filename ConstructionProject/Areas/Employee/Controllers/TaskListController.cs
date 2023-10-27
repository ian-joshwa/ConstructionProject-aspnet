using System.Collections.Generic;
using Construction.CommonHelper;
using System.Data;
using Construction.CommonHelper.Enum;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Construction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ConstructionProject.Areas.Employee.Controllers
{

    [Area("Employee")]
    [Authorize(Roles = WebsiteRole.Role_Admin + "," + WebsiteRole.Role_Employee)]
    public class TaskListController : Controller
    {

        private IUnitOfWork _unitOfWork;

        public TaskListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

		#region APICALL

        public IActionResult GetAllTask()
        {
            var Tasks = _unitOfWork.TaskList.GetAll(includeProperties: "Project");
            return Json(new { data = Tasks });
        }

		#endregion

		[HttpGet]
        public IActionResult Add()
        {
            TaskListVM vm = new TaskListVM();
            var list = new List<SelectListItem>();
            foreach(var status in Enum.GetValues(typeof(StatusTypes)))
            {
                list.Add(new SelectListItem()
                {
                    Text = status.ToString(),
                    Value = status.ToString()
                });
            }

            vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            vm.Status = list;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(TaskListVM vm)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TaskList.Add(vm.TaskList);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(vm);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            TaskListVM vm = new TaskListVM();
			var list = new List<SelectListItem>();
			vm.TaskList = _unitOfWork.TaskList.GetById(x => x.Id == id);
			foreach (var status in Enum.GetValues(typeof(StatusTypes)))
			{
				list.Add(new SelectListItem()
				{
					Text = status.ToString(),
					Value = status.ToString()
				});
			}

			vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});
			vm.Status = list;

			return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TaskListVM vm)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.TaskList.Update(vm.TaskList);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            TaskListVM vm = new TaskListVM();
            vm.TaskList = _unitOfWork.TaskList.GetById(x => x.Id == id);

			var list = new List<SelectListItem>();
			foreach (var status in Enum.GetValues(typeof(StatusTypes)))
			{
				list.Add(new SelectListItem()
				{
					Text = status.ToString(),
					Value = status.ToString()
				});
			}

			vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});
			vm.Status = list;

			return View(vm);

        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {
            TaskListVM vm = new TaskListVM();
            vm.TaskList = _unitOfWork.TaskList.GetById(x => x.Id == id);
            _unitOfWork.TaskList.Delete(vm.TaskList);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


    }
}
