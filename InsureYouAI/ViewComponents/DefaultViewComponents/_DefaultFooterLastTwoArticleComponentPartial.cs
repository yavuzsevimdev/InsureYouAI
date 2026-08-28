using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultFooterLastTwoArticleComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DefaultFooterLastTwoArticleComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Articles.OrderByDescending(x => x.ArticleId).Skip(3).Take(2).ToList();
            return View(values);
        }
    }
}
