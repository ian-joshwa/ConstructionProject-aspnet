using Construction.CommonHelper;
using Construction.DataAccessLayer.Data;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Construction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ServiceController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private IWebHostEnvironment _hostingEnvironment;

		public ServiceController(IUnitOfWork unitOfWork,
			IWebHostEnvironment hostingEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostingEnvironment = hostingEnvironment;
		}

		#region SERAPICALL

		public IActionResult GetAllServices()
		{
			var services = _unitOfWork.Service.GetAll();
			return Json(new { data = services });
		}

        #endregion


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpGet]
		public IActionResult Home()
		{
			return View();
		}


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpGet]
		public IActionResult Add()
		{
			return View();
		}


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpPost]
		public IActionResult Add(ServiceVM vm, IFormFile? file)
		{

			string fileName = "";
			if (file != null)
			{
				string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ServiceImages");
				fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
				string filePath = Path.Combine(uploadDir, fileName);
				using (var filestream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(filestream);
				}
				vm.Service.ImageUrl = @"\ServiceImages\" + fileName;
			}

			if (ModelState.IsValid)
			{
				_unitOfWork.Service.Add(vm.Service);
				_unitOfWork.Save();
				return RedirectToAction("Home");
			}

			return View(vm);
		}


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpGet]
		public IActionResult Edit(int? id)
		{
			ServiceVM vm = new ServiceVM();
			vm.Service = _unitOfWork.Service.GetById(x => x.Id == id);

			return View(vm);
		}



        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpPost]
		public IActionResult Edit(ServiceVM vm, IFormFile? file)
		{
			if (ModelState.IsValid)
			{

				string fileName = "";
				if (file != null)
				{
					string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ServiceImages");
					fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
					string filePath = Path.Combine(uploadDir, fileName);

					if (vm.Service.ImageUrl != null)
					{
						var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Service.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var filestream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(filestream);
					}
					vm.Service.ImageUrl = @"\ServiceImages\" + fileName;
				}

				_unitOfWork.Service.Update(vm.Service);
				_unitOfWork.Save();
				return RedirectToAction("Home");

			}

			return View(vm);
		}


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpGet]
		public IActionResult Delete(int? id)
		{
			ServiceVM vm = new ServiceVM();
			vm.Service = _unitOfWork.Service.GetById(x => x.Id == id);

			return View(vm);
		}



        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpPost, ActionName("Delete")]
		public IActionResult DeleteData(int? id)
		{
			ServiceVM vm = new ServiceVM();
			vm.Service = _unitOfWork.Service.GetById(x => x.Id == id);
			if (vm.Service.ImageUrl != null)
			{
				var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Service.ImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}
			}

			_unitOfWork.Service.Delete(vm.Service);
			_unitOfWork.Save();
			return RedirectToAction("Home");


		}

		[HttpGet]
		public IActionResult Detail(int? id)
		{
			ServiceVM vm = new ServiceVM();
			vm.Service = _unitOfWork.Service.GetById(x => x.Id == id);

			return View(vm);
		}

	}
}
