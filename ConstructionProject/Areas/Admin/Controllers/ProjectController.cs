using Construction.CommonHelper;
using System.Data;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Construction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class ProjectController : Controller
    {

        private IWebHostEnvironment _hostingEnvironment;
        private IUnitOfWork _unitOfWork;

        public ProjectController(IWebHostEnvironment hostEnvironment,
            IUnitOfWork unitOfWork)
        {
            _hostingEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
        }


		#region PRJAPICALL

        public IActionResult GetAllProjects()
        {
            var projects = _unitOfWork.Project.GetAll();
            return Json(new { data =  projects });
        }

        #endregion


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpPost]
        public IActionResult Add(ProjectVM vm, IFormFile? file)
        {
			string fileName = "";
			if (file != null)
			{
				string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProjectImages");
				fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
				string filePath = Path.Combine(uploadDir, fileName);
				using (var filestream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(filestream);
				}
				vm.Project.ImageUrl = @"\ProjectImages\" + fileName;
			}

			if (ModelState.IsValid)
            {

                _unitOfWork.Project.Add(vm.Project);
                _unitOfWork.Save();
                return RedirectToAction("Home");
            }
             
            return View(vm);
        }

        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ProjectVM vm = new ProjectVM();
            vm.Project = _unitOfWork.Project.GetById(x => x.Id == id);
            
            return View(vm);
        }


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpPost]
        public IActionResult Edit(ProjectVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

				string fileName = "";
				if (file != null)
				{
					string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProjectImages");
					fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
					string filePath = Path.Combine(uploadDir, fileName);

					if (vm.Project.ImageUrl != null)
					{
						var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Project.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var filestream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(filestream);
					}
					vm.Project.ImageUrl = @"\ProjectImages\" + fileName;
				}

                _unitOfWork.Project.Update(vm.Project);
                _unitOfWork.Save();
                return RedirectToAction("Home");

			}

            return View(vm);
        }

        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ProjectVM vm = new ProjectVM();
            vm.Project = _unitOfWork.Project.GetById(x => x.Id == id);

            return View(vm);
        }


        [Authorize(Roles = WebsiteRole.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {

            ProjectVM vm = new ProjectVM();
            vm.Project = _unitOfWork.Project.GetById(x => x.Id == id);

			if (vm.Project.ImageUrl != null)
			{
				var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Project.ImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}
			}

            _unitOfWork.Project.Delete(vm.Project);
            _unitOfWork.Save();

            return RedirectToAction("Home");


		}

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            ProjectVM vm = new ProjectVM();
            vm.Project = _unitOfWork.Project.GetById(x => x.Id == id);

            return View(vm);
        }
    }
}
