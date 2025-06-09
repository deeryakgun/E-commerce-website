using Microsoft.AspNetCore.Mvc;
using e_commerce.Data;

namespace e_commerce.Component
{
    public class SliderList : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SliderList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.Slider.ToList();
            return View(result);
        }
    }
}