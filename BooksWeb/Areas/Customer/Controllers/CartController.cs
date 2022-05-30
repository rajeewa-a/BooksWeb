using BooksWeb.Models;
using BooksWeb.Models.ViewModels;
using BooksWeb.Repository.IRepository;
using BooksWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using Session = Stripe.Checkout.Session;

namespace BooksWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        [BindProperty]
        public CartVM CartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM = new CartVM()
            {
                ListCart = _unitOfWork.Cart.GetAll(u => u.AppUserId == claim.Value,
                includeProperties: "Book"),
                OrderHeader = new()
            };
            foreach (var cart in CartVM.ListCart)
            {
                cart.Price = cart.Book.Price;
                CartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(CartVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM = new CartVM()
            {
                ListCart = _unitOfWork.Cart.GetAll(u => u.AppUserId == claim.Value,
                includeProperties: "Book"),
                OrderHeader = new()
            };
            CartVM.OrderHeader.AppUser = _unitOfWork.AppUser.GetFirstOrDefault(
                u => u.Id == claim.Value);

            CartVM.OrderHeader.Name = CartVM.OrderHeader.AppUser.Name;
            CartVM.OrderHeader.PhoneNumber = CartVM.OrderHeader.AppUser.PhoneNumber;
            CartVM.OrderHeader.Address = CartVM.OrderHeader.AppUser.Address;
            CartVM.OrderHeader.PostalCode = CartVM.OrderHeader.AppUser.PostalCode;



            foreach (var cart in CartVM.ListCart)
            {
                cart.Price = cart.Book.Price;
                CartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(CartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM.ListCart = _unitOfWork.Cart.GetAll(u => u.AppUserId == claim.Value,
                includeProperties: "Book");


            CartVM.OrderHeader.OrderDate = System.DateTime.Now;
            CartVM.OrderHeader.AppUserId = claim.Value;


            foreach (var cart in CartVM.ListCart)
            {
                cart.Price = cart.Book.Price;
                CartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            AppUser applicationUser = _unitOfWork.AppUser.GetFirstOrDefault(u => u.Id == claim.Value);

            CartVM.OrderHeader.OrderStatus = SD.StatusApproved;

            _unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
            _unitOfWork.Save();


            foreach (var cart in CartVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    BookId = cart.BookId,
                    OrderId = CartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };

                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }


                //stripe 
                var domain = "https://localhost:44300/";
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={CartVM.OrderHeader.Id}",
                    CancelUrl = domain + $"customer/cart/index",
                };

                foreach (var item in CartVM.ListCart)
                {

                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "lkr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Book.Title
                            },

                        },
                        Quantity = item.Count,
                    };
                    options.LineItems.Add(sessionLineItem);

                }

                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeader.UpdateStripePaymentID(CartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);

        }

        public IActionResult OrderConfirmation(int id)
        {
                OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "AppUser");

                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
            
                _emailSender.SendEmailAsync(orderHeader.AppUser.Email, "New Order - BooksWeb", "<p>New Order Created</p>");
                List<Cart> carts = _unitOfWork.Cart.GetAll(u => u.AppUserId == orderHeader.AppUserId).ToList();
                _unitOfWork.Cart.RemoveRange(carts);
                _unitOfWork.Save();
                return View(id);
        }

        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.Cart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.Cart.Remove(cart);
                var count = _unitOfWork.Cart.GetAll(u => u.AppUserId == cart.AppUserId).ToList().Count - 1;
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            else
            {
                _unitOfWork.Cart.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.Cart.Remove(cart);
            _unitOfWork.Save();
            var count = _unitOfWork.Cart.GetAll(u => u.AppUserId == cart.AppUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToAction(nameof(Index));
        }

    }
}
