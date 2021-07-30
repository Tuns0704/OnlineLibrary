using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.DataAccess.Repository.IRepository;
using OnlineLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLibrary.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using OnlineLibrary.Utility;

namespace OnlineLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

    public class ChapterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ChapterController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ChapterVM chapterVM = new ChapterVM()
            {
                Chapter = new Chapter(),
                DocumentList = _unitOfWork.Document.GetAll().Select(i => new SelectListItem {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }),
            };
            if(id == null)
            {
                return View(chapterVM);
            }
            chapterVM.Chapter = _unitOfWork.Chapter.Get(id.GetValueOrDefault());
            if(chapterVM.Chapter == null)
            {
                return NotFound();
            }
            return View(chapterVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ChapterVM chapterVM)
        {
            if (ModelState.IsValid)
            {
                if (chapterVM.Chapter.Id == 0)
                {
                    _unitOfWork.Chapter.Add(chapterVM.Chapter);

                }
                else
                {
                    _unitOfWork.Chapter.Update(chapterVM.Chapter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {   chapterVM.DocumentList = _unitOfWork.Document.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                });
                if (chapterVM.Chapter.Id != 0)
                {
                    chapterVM.Chapter = _unitOfWork.Chapter.Get(chapterVM.Chapter.Id);
                }
            }
            return View(chapterVM);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Chapter.GetAll(includeProperties: "Document");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Chapter.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Chapter.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
