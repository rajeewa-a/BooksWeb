using BooksWeb.Models.ViewModels;
using BooksWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
    /*[Authorize(Roles = SD.Role_Admin)]*/
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            BookVM bookVM = new()
            {
                Book = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {

                return View(bookVM);
            }
            else
            {
                bookVM.Book = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id);
                return View(bookVM);

                //update product
            }


        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\books");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Book.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Book.ImageUrl = @"\images\books\" + fileName + extension;

                }
                if (obj.Book.Id == 0)
                {
                    _unitOfWork.Book.Add(obj.Book);
                }
                else
                {
                    _unitOfWork.Book.Update(obj.Book);
                }
                _unitOfWork.Save();
                TempData["success"] = "Book created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var bookList = _unitOfWork.Book.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = bookList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Book.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}