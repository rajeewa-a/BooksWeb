using BooksWeb.Models;
using BooksWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Feedback> objFeedbackList = _unitOfWork.Feedback.GetAll();
            return View(objFeedbackList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Feedback.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Feedback Sent!";
                return RedirectToAction("Index","Home", new {area = "Customer"});
            }
            return View(obj);
        }
    }
}
