using System.Security.Claims;
using Construction.CommonHelper;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Construction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConstructionProject.Areas.Employee.Controllers
{
	[Area("Employee")]
	[Authorize(Roles = WebsiteRole.Role_Admin + "," + WebsiteRole.Role_Employee)]
	public class SiteUpdateController : Controller
    {
        IUnitOfWork _unitOfWork;
        IWebHostEnvironment _hostingEnvironment;

        public SiteUpdateController(IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region APICALL

        public IActionResult GetSiteUpdates()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _unitOfWork.ApplicationUser.GetById(x => x.Id == userid);

            var adminuser = _unitOfWork.ApplicationUser.GetAll().Where(x => x.Email == "superadmin@xyz.com").FirstOrDefault();

            if(user.Email == "superadmin@xyz.com")
            {
                var adminUpdates = _unitOfWork.SiteUpdates.GetAll(includeProperties: "Project,ApplicationUser");
                return Json(new { data = adminUpdates });
            }
            else
            {
                var siteUpdates = _unitOfWork.SiteUpdates.GetAll(includeProperties: "Project,ApplicationUser").Where(x => x.ApplicationUserId == userid || x.ApplicationUserId == adminuser.Id);
                return Json(new { data = siteUpdates });
            }
            

        }


        #endregion


        [HttpGet]
        public IActionResult Add(SiteUpdateVM vm)
        {
            vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(SiteUpdateVM vm, IFormFile? file)
        {


            string fileName = "";
            if (file != null)
            {
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "SiteUpdateImages");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                vm.SiteUpdate.ImageUrl = @"\SiteUpdateImages\" + fileName;
            }
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var APUser = _unitOfWork.ApplicationUser.GetById(x => x.Id == id);

            if (ModelState.IsValid)
            {
                vm.SiteUpdate.Date = DateTime.Now;
                vm.SiteUpdate.ApplicationUserId = APUser.Id;
                _unitOfWork.SiteUpdates.Add(vm.SiteUpdate);
                _unitOfWork.Save();

                return RedirectToAction("Index");

            }

            return View(vm);

        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {

            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _unitOfWork.ApplicationUser.GetById(x => x.Id == userid);

			SiteUpdateVM vm = new SiteUpdateVM();

			vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});

			vm.SiteUpdate = _unitOfWork.SiteUpdates.GetById(x => x.Id == id);

			if (user.Email == "superadmin@xyz.com")
            {

				return View(vm);

			}

            if(vm.SiteUpdate.ApplicationUserId ==  userid)
            {
				return View(vm);
			}

            return RedirectToAction("Index");

        }


        [HttpPost]
        public IActionResult Edit(SiteUpdateVM vm, IFormFile? file)
        {

			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			vm.SiteUpdate.ApplicationUserId = userid;

			if (ModelState.IsValid)
            {
				string fileName = "";
				if (file != null)
				{
					string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "SiteUpdateImages");
					fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
					string filePath = Path.Combine(uploadDir, fileName);

					if (vm.SiteUpdate.ImageUrl != null)
					{
						var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.SiteUpdate.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}
					using (var filestream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(filestream);
					}
					vm.SiteUpdate.ImageUrl = @"\SiteUpdateImages\" + fileName;
				}

				vm.SiteUpdate.Date = DateTime.Now;
                _unitOfWork.SiteUpdates.Update(vm.SiteUpdate);
                _unitOfWork.Save();

				return RedirectToAction("Index");
			}

            return View(vm);

        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {

            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _unitOfWork.ApplicationUser.GetById(x => x.Id == userid);

            SiteUpdateVM vm = new SiteUpdateVM();

            vm.Projects = _unitOfWork.Project.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            vm.SiteUpdate = _unitOfWork.SiteUpdates.GetById(x => x.Id == id);

            if (user.Email == "superadmin@xyz.com")
            {

                return View(vm);

            }

            if (vm.SiteUpdate.ApplicationUserId == userid)
            {
                return View(vm);
            }

            return RedirectToAction("Index");

        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {

            SiteUpdateVM vm = new SiteUpdateVM();

            vm.SiteUpdate = _unitOfWork.SiteUpdates.GetById(x => x.Id == id);
            if(vm.SiteUpdate.ImageUrl != null) {
				var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.SiteUpdate.ImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}
			}
            _unitOfWork.SiteUpdates.Delete(vm.SiteUpdate);
            _unitOfWork.Save();

            return RedirectToAction("Index");   

        }
    }
}
