using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Data;


namespace e_commerce.Controllers
{
    public class ShopController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var result = _context.Products.ToList();
            return View(result);
        }
    }
}

