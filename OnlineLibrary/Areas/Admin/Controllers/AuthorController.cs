using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.DataAccess.Repository.IRepository;
using OnlineLibrary.Models;
using OnlineLibrary.Utility;

namespace OnlineLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + ","+ SD.Role_Employee)]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Author author = new Author();
            if(id == null)
            {
                return View(author);
            }
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            author = _unitOfWork.SP_Call.OneRecord<Author>(SD.Proc_Author_Get, parameter);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author author)
        {
            if(ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", author.Name);
                if(author.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.Proc_Author_Create, parameter);
                }
                else
                {
                    parameter.Add("@Id", author.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Author_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SP_Call.List<Author>(SD.Proc_Author_GetAll,null);
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<Author>(SD.Proc_Author_Get,parameter);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_Author_Delete,parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
