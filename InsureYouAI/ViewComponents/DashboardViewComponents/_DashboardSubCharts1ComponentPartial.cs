using InsureYouAI.Context;
using InsureYouAI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace InsureYouAI.ViewComponents.DashboardViewComponents
{
    public class _DashboardSubCharts1ComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DashboardSubCharts1ComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var policyData = _context.Policies
                .GroupBy(p => p.PolicyType)
                .Select(g => new
                {
                    PolicyType = g.Key,
                    Count = g.Count()
                }).ToList();
            ViewBag.policyData = JsonConvert.SerializeObject(policyData);

            return View();
        }
    }
}
