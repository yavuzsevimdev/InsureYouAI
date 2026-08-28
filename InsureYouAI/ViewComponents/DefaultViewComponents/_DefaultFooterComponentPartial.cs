using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultFooterComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DefaultFooterComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Description = _context.Contacts.Select(x => x.Description).FirstOrDefault();
            ViewBag.Phone = _context.Contacts.Select(x => x.Phone).FirstOrDefault();
            ViewBag.Email = _context.Contacts.Select(x => x.Email).FirstOrDefault();
            ViewBag.Address = _context.Contacts.Select(x => x.Address).FirstOrDefault();
            return View();
        }
    }
}
