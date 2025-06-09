using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Data;
using e_commerce.Models;

namespace e_commerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SliderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Slider
        public async Task<IActionResult> Index()
        {
              return _context.Slider != null ? 
                          View(await _context.Slider.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Slider'  is null.");
        }

        // GET: Slider/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Slider/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SliderId,SliderName,Header1,Header2,Context,SliderImage")] Slider slider, IFormFile ImageUpload)
        {

            if (ImageUpload != null)
            {
                var uzanti = Path.GetExtension(ImageUpload.FileName);
                //bocek.png  .png domates.jpg  .jpg
                string yeniisim = Guid.NewGuid().ToString() + uzanti;

                string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Slider/" + yeniisim);
                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    ImageUpload.CopyToAsync(stream);
                }
                slider.SliderImage = yeniisim;
            }


            if (ModelState.IsValid)
            {
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Slider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SliderId,SliderName,Header1,Header2,Context,SliderImage")] Slider slider, IFormFile ImageUpload)
        {

            if (ImageUpload != null)
            {
                var uzanti = Path.GetExtension(ImageUpload.FileName);
                //bocek.png  .png domates.jpg  .jpg
                string yeniisim = Guid.NewGuid().ToString() + uzanti;

                string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Slider/" + yeniisim);
                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    ImageUpload.CopyToAsync(stream); 
                }
                slider.SliderImage = yeniisim;
            }


            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Slider == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slider'  is null.");
            }
            var slider = await _context.Slider.FindAsync(id);
            if (slider != null)
            {
                _context.Slider.Remove(slider);
            }

            //Dosya silme
            string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Slider/" + slider.SliderImage);
            FileInfo yolFile = new FileInfo(yol);
            if (yolFile.Exists)
            {
                System.IO.File.Delete(yolFile.FullName);
                yolFile.Delete();
            }
            //Dosya Silme

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return (_context.Slider?.Any(e => e.SliderId == id)).GetValueOrDefault();
        }
    }
}
