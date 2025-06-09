using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Data;
using e_commerce.Models;
using e_commerce.Oturum;
using e_commerce.Dto;


namespace e_commerce.Component
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItem> items = HttpContext.Session.GetJson <List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartvm = new()
            {
                CartItems = items,
                GrandTotal = items.Sum(x => x.Quantity * x.Price)
            };
            return View(cartvm);
        }

        public async Task<IActionResult> Add (int id)
        {
            Products product = _context.Products.Find(id);
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = items.FirstOrDefault(x => x.ProductId == id);

            if(cartItem == null)
            {
                items.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }
            

            HttpContext.Session.SetJson("Cart", items);
            TempData["Mesaj"] = "Ürün Sepete Eklenmiştir.";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease (int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if(cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }
            if(cart.Count() > 0)
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["Mesaj"] = "Ürün Sepetten Silindi";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            cart.RemoveAll(x => x.ProductId == id);
            if(cart.Count() > 0)
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            else
            {
                HttpContext.Session.Remove("Cart");
            }

            TempData["Mesaj"] = "Ürün Sepetten Kaldırıldı.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        
    }
}

