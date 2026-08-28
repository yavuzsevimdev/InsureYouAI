using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DefaultAboutComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Title = _context.Abouts.Select(x => x.Title).FirstOrDefault();
            ViewBag.Description = _context.Abouts.Select(x => x.Description).FirstOrDefault();
            ViewBag.ImageUrl = _context.Abouts.Select(x => x.ImageUrl).FirstOrDefault();

            var aboutItemValues = _context.AboutItems.ToList();
            return View(aboutItemValues);
        }
    }
}
