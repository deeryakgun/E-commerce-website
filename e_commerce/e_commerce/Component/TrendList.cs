using e_commerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Component
{
    public class TrendList : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public TrendList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.Products.ToList();
            return View(result);
        }
    }
}