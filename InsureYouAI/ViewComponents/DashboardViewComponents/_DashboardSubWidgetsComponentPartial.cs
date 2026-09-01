using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DashboardViewComponents
{
    public class _DashboardSubWidgetsComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DashboardSubWidgetsComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.TotalCategoryCount = _context.Categories.Count();
            ViewBag.TotalArticleCount = _context.Articles.Count();
            ViewBag.TotalPolicyCount = _context.Policies.Count();
            ViewBag.TotalPolicyByThisMonthCount = _context.Policies.Where(x => x.StartDate.Month == DateTime.Now.Month).Count();
            ViewBag.TotalCommentCount = _context.Comments.Count();
            ViewBag.TotalUserCount = _context.Users.Count();
            ViewBag.AvgPolicyAmount = _context.Policies.Average(x => x.PremiumAmount);
            ViewBag.LastRevenueAmount = _context.Revenues.OrderByDescending(x => x.RevenueId).Take(1).Select(y => y.Amount).FirstOrDefault();

            return View();
        }
    }
}
