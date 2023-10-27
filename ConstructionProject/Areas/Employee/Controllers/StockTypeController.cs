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
    public class StockTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public StockTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


		#region APICALL

        public IActionResult GetStockTypes()
        {
            var stockTypes = _unitOfWork.StockType.GetAll();
            return Json(new { data = stockTypes });
        }

        #endregion


		public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Add()
        {
            StockTypeVM vm = new StockTypeVM();
            var list = new List<SelectListItem>();
            foreach(var unit in Enum.GetValues(typeof(StockTypeUnits)))
            {
                list.Add(new SelectListItem()
                {
                    Text = unit.ToString(),
                    Value = unit.ToString()
                });
            }

            vm.StockTypeUnits = list;

            return View(vm);
        }



        [HttpPost]
        public IActionResult Add(StockTypeVM vm)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StockType.Add(vm.StockType);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(vm);
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            StockTypeVM vm = new StockTypeVM();
            vm.StockType = _unitOfWork.StockType.GetById(x => x.Id == id);
			var list = new List<SelectListItem>();
			foreach (var unit in Enum.GetValues(typeof(StockTypeUnits)))
			{
				list.Add(new SelectListItem()
				{
					Text = unit.ToString(),
					Value = unit.ToString()
				});
			}

			vm.StockTypeUnits = list;
			return View(vm);
        }



        [HttpPost]
        public IActionResult Edit(StockTypeVM vm)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.StockType.Update(vm.StockType);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(vm);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {

            StockTypeVM vm = new StockTypeVM();
            vm.StockType = _unitOfWork.StockType.GetById(x => x.Id == id);

            return View(vm);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {

            StockTypeVM vm = new StockTypeVM();
            vm.StockType = _unitOfWork.StockType.GetById(x => x.Id == id);
            _unitOfWork.StockType.Delete(vm.StockType);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

    }
}
