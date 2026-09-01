using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailNextAndPrevBlogComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _BlogDetailNextAndPrevBlogComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var prevArticle = _context.Articles
                .Where(x => x.ArticleId < id)
                .OrderByDescending(x => x.ArticleId)
                .FirstOrDefault();
                
            var nextArticle = _context.Articles
                .Where(x => x.ArticleId > id)
                .OrderBy(x => x.ArticleId)
                .FirstOrDefault();

            prevArticle ??= new Article
            {
                ArticleId = 0,
                Title = "-"
            };

            nextArticle ??= new Article
            {
                ArticleId = 0,
                Title = "-"
            };

            ViewBag.PrevArticle = prevArticle;
            ViewBag.NextArticle = nextArticle;


            return View();
        }
    }
}
