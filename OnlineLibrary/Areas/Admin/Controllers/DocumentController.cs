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

    public class DocumentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DocumentController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
            DocumentVM documentVM = new DocumentVM()
            {
                Document = new Document(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                AuthorList = _unitOfWork.Author.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if(id == null)
            {
                return View(documentVM);
            }
            documentVM.Document = _unitOfWork.Document.Get(id.GetValueOrDefault());
            if(documentVM.Document == null)
            {

                return NotFound();
            }
            return View(documentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DocumentVM documentVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (documentVM.Document.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, documentVM.Document.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    documentVM.Document.ImageUrl = @"\images\products\" + fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (documentVM.Document.Id != 0)
                    {
                        Document objFromDb = _unitOfWork.Document.Get(documentVM.Document.Id);
                        documentVM.Document.ImageUrl = objFromDb.ImageUrl;
                    }
                }


                if (documentVM.Document.Id == 0)
                {
                    _unitOfWork.Document.Add(documentVM.Document);

                }
                else
                {
                    _unitOfWork.Document.Update(documentVM.Document);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {   documentVM.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                documentVM.AuthorList = _unitOfWork.Author.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                if (documentVM.Document.Id != 0)
                {
                    documentVM.Document = _unitOfWork.Document.Get(documentVM.Document.Id);
                }
            }
            return View(documentVM);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Document.GetAll(includeProperties: "Category,Author");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Document.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.Document.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
