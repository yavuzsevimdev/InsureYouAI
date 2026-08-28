using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailContentComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _BlogDetailContentComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var value = _context.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            return View(value);
        }
    }
}
