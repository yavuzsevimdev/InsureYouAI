using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.BlogViewComponents
{
    public class _BlogListThreeRecentBlogComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _BlogListThreeRecentBlogComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Articles.OrderByDescending(x => x.ArticleId).Take(3).ToList();
            return View(values);
        }
    }
}
