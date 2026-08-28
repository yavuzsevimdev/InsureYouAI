using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.Controllers
{
    public class SliderController : Controller
    {
        private readonly InsureContext _context;

        public SliderController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult SliderList()
        {
            var values = _context.Sliders.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSlider(Slider service)
        {
            _context.Sliders.Add(service);
            _context.SaveChanges();
            return RedirectToAction("SliderList");
        }

        [HttpGet]
        public IActionResult UpdateSlider(int id)
        {
            var value = _context.Sliders.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateSlider(Slider service)
        {
            _context.Sliders.Update(service);
            _context.SaveChanges();
            return RedirectToAction("SliderList");
        }

        public IActionResult DeleteSlider(int id)
        {
            var value = _context.Sliders.Find(id);
            _context.Sliders.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("SliderList");
        }
    }
}
