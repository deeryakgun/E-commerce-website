using System;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Data;
using e_commerce.Dto;
using e_commerce.Models;
using e_commerce.Oturum;

namespace e_commerce.Component
{
	public class CartSumList : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public CartSumList(ApplicationDbContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Quantity * x.Price)

			};

			return View(cartVM);

        }
	}
}

