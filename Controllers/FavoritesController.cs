using DnTech_Ecommerce.Data;
using DnTech_Ecommerce.Models;
using DnTech_Ecommerce.ViewModels;
using DnTech_ECommerce.Data;
using DnTech_ECommerce.Models;
using DnTech_ECommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace DnTech_ECommerce.Controllers
{
    public class FavoritesController : Controller
    {           
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
     

        public FavoritesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            
        }

        // GET: Display all favorites for current user
        public ActionResult Index()
        {
            string? userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

             var favorites = _context.Favorites
                .Include(f => f.Product)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .ToList();

            var viewModel = favorites.Select(f => new FavoriteViewModel
            {
                FavoriteId = f.Id,
                ProductId = f.ProductId,
                ProductName = f.Product?.Name ?? string.Empty,
                CurrentPrice = f.Product?.Price ?? f.Product?.Price ?? 0m,
                OriginalPrice = f.PriceWhenAdded,
                ImageUrl = f.Product?.MainImageUrl,
                IsOnSale = f.Product?.IsOnSale ?? false,
                NotifyOnSale = f.NotifyOnSale,
                DateAdded = f.CreatedAt,
                PriceDropped = (f.Product?.OldPrice ?? f.Product?.OldPrice ?? 0m) < f.PriceWhenAdded
            }).ToList();

            return View(favorites);
        }

        // POST: Add to favorites (with toggle support)
        [HttpPost]
        public ActionResult Toggle(int productId)
        {
            string? userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "User not authenticated" });
            }

            var existingFavorite = _context.Favorites
                .FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);

            if (existingFavorite != null)
            {
                // Remove from favorites
                _context.Favorites.Remove(existingFavorite);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    isFavorited = false,
                    message = "Removed from favorites"
                });
            }
            else
            {
                // Add to favorites
                var product = _context.Products.Find(productId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found" });
                }

                var favorite = new Favorite
                {
                    UserId = userId,
                    ProductId = productId,
                    CreatedAt = DateTime.Now,
                    PriceWhenAdded = product.OldPrice ?? product.Price,
                    NotifyOnSale = true  // Default to true
                };

                _context.Favorites.Add(favorite);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    isFavorited = true,
                    message = "Added to favorites!"
                });
            }
        }

        // POST: Update notification preference
        [HttpPost]
        public ActionResult UpdateNotification(int favoriteId, bool notify)
        {
            var favorite = _context.Favorites.Find(favoriteId);
            if (favorite != null && favorite.UserId == GetCurrentUserId())
            {
                favorite.NotifyOnSale = notify;
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        // GET: Check if product is favorited
        [HttpGet]
        public ActionResult IsFavorited(int productId)
        {
            string? userId = GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { isFavorited = false });
            }

            bool isFavorited = _context.Favorites
                .Any(f => f.UserId == userId && f.ProductId == productId);

            return Json(new { isFavorited = isFavorited });
        }


       //POST: Remove from favorites
       [HttpPost]
        public ActionResult Remove(int productId)
        {
            string? userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "User not authenticated" });
            }

            var favorite = _context.Favorites
                .FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();

                return Json(new { success = true, message = "Removed from favorites" });
            }

            return Json(new { success = false, message = "Item not found" });
        }


        // GET: Get favorites count (for navbar badge)
        [HttpGet]
        public ActionResult GetCount()
        {
            string? userId = GetCurrentUserId();
            int count = _context.Favorites.Count(f => f.UserId == userId);

            // Remove JsonRequestBehavior.AllowGet for ASP.NET Core
            return Json(new { count = count });
        }


        // Replace the GetCurrentUserId method with the following implementation
        private string? GetCurrentUserId()
        {
            // ASP.NET Core: Get user ID from claims
            return User?.Identity?.IsAuthenticated == true
                ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                : null;
        }
    }
}
