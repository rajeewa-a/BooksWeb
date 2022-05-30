using BooksWeb.Models;
using BooksWeb.Models.ViewModels;
using BooksWeb.Repository.IRepository;
using BooksWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BooksWeb.Controllers;
[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Book> bookList = _unitOfWork.Book.GetAll(includeProperties: "Category,CoverType");

        return View(bookList);
    }

    public IActionResult Details(int bookId)
    {
        Cart cartObj = new()
        {
            Count = 1,
            BookId = bookId,
            Book = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == bookId, includeProperties: "Category,CoverType"),
        };

        return View(cartObj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(Cart cart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        cart.AppUserId = claim.Value;

        Cart cartFromDb = _unitOfWork.Cart.GetFirstOrDefault(
            u => u.AppUserId == claim.Value && u.BookId == cart.BookId);


        if (cartFromDb == null)
        {

            _unitOfWork.Cart.Add(cart);
            _unitOfWork.Save();
/*            HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.Cart.GetAll(u => u.AppUserId == claim.Value).ToList().Count);*/
        }
        else
        {
            _unitOfWork.Cart.IncrementCount(cartFromDb, cart.Count);
            _unitOfWork.Save();
        }


        return RedirectToAction(nameof(Index));
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}