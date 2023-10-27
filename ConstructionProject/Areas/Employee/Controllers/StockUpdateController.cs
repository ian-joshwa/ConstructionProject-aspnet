using Construction.CommonHelper;
using System.Data;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Construction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConstructionProject.Areas.Employee.Controllers
{
	[Area("Employee")]
    [Authorize(Roles = WebsiteRole.Role_Admin + "," + WebsiteRole.Role_Employee)]
    public class StockUpdateController : Controller
	{

		private IUnitOfWork _unitOfWork;

		public StockUpdateController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		#region APICALL 

		public IActionResult GetStockUpdates()
		{
			var stockupdates = _unitOfWork.StockUpdate.GetAll(includeProperties: "Project");
            return Json(new { data = stockupdates });
		}

        #endregion

        public IActionResult Index()
		{
			return View();
		}


        [HttpGet]
		public IActionResult Add()
		{
			StockUpdatesVM vm = new StockUpdatesVM();
			vm.StockType = _unitOfWork.StockType.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Name
			});

			vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});

			return View(vm);
		}


        [HttpPost]
		public IActionResult Add(StockUpdatesVM vm)
		{
			if (ModelState.IsValid)
			{
				vm.StockUpdate.Date = DateTime.Now;
				_unitOfWork.StockUpdate.Add(vm.StockUpdate);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}

			return View(vm);
		}


        [HttpGet]
		public IActionResult Edit(int? id)
		{
			StockUpdatesVM vm = new StockUpdatesVM();
			vm.StockUpdate = _unitOfWork.StockUpdate.GetById(x => x.Id == id);
			vm.StockType = _unitOfWork.StockType.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Name
			});

			vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});

			return View(vm);
		}


        [HttpPost]
		public IActionResult Edit(StockUpdatesVM vm)
		{
			if (ModelState.IsValid)
			{
				vm.StockUpdate.Date = DateTime.Now;
				_unitOfWork.StockUpdate.Update(vm.StockUpdate);
				_unitOfWork.Save();

				return RedirectToAction("Index");
			}

			return View(vm);
		}


        [HttpGet]
		public IActionResult Delete(int? id)
		{
			StockUpdatesVM vm = new StockUpdatesVM();
			vm.StockUpdate = _unitOfWork.StockUpdate.GetById(x => x.Id == id);
			vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});

			return View(vm);
		}


        [HttpPost,ActionName("Delete")]
		public IActionResult DeleteData(int? id)
		{
			StockUpdatesVM vm = new StockUpdatesVM();
			vm.StockUpdate = _unitOfWork.StockUpdate.GetById(x => x.Id == id);
			_unitOfWork.StockUpdate.Delete(vm.StockUpdate);
			_unitOfWork.Save();

			return RedirectToAction("Index");
		}
	}
}
