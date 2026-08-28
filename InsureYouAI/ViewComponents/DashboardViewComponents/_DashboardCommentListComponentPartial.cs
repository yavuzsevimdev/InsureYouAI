using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAI.ViewComponents.DashboardViewComponents
{
    public class _DashboardCommentListComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DashboardCommentListComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Comments.Include(x => x.AppUser).OrderByDescending(y => y.CommentDate).Take(7).ToList();
            return View(values);
        }
    }
}
