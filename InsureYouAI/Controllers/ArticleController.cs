using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly InsureContext _context;

        public ArticleController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult ArticleList()
        {
            ViewBag.ControllerName = "Makaleler";
            ViewBag.PageName = "Makale Listesi";
            var values = _context.Articles.Include(x => x.AppUser).Include(x => x.Category).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            ViewBag.ControllerName = "Makaleler";
            ViewBag.PageName = "Yeni Makale Oluştur";

            var categories = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                }).ToList();
            ViewBag.Categories = categories;

            var authors = _context.Users
                .Select(x => new SelectListItem
                {
                    Text = x.Name + " "+ x.Surname,
                    Value = x.Id
                }).ToList();

            ViewBag.Authors = authors;

            return View();
        }

        [HttpPost]
        public IActionResult CreateArticle(Article article)
        {
            article.CreatedDate = DateTime.Now;
            _context.Articles.Add(article);
            _context.SaveChanges();
            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public IActionResult UpdateArticle(int id)
        {
            ViewBag.ControllerName = "Makaleler";
            ViewBag.PageName = "Makale Güncelleme Sayfası";

            var categories = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                }).ToList();
            ViewBag.Categories = categories;

            var authors = _context.Users
                .Select(x => new SelectListItem
                {
                    Text = x.Name + " " + x.Surname,
                    Value = x.Id
                }).ToList();

            ViewBag.Authors = authors;

            var value = _context.Articles.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
            _context.SaveChanges();
            return RedirectToAction("ArticleList");
        }

        public IActionResult DeleteArticle(int id)
        {
            var value = _context.Articles.Find(id);
            _context.Articles.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ArticleList");
        }



        [HttpGet]
        public IActionResult CreateArticleWithOpenAI()
        {
            ViewBag.ControllerName = "Makaleler";
            ViewBag.PageName = "Yapay Zeka Makale Oluşturucu";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticleWithOpenAI(string prompt)
        {
            var apiKey = "";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-4o-mini",
                instructions = "Sen bir sigorta şirketi için çalışan, içerik yazarlığı yapan bir yapay zekasın. Kullanıcının verdiği özet ve anahtar kelimelere göre sigortacılık sektörüyle ilgili makale üret. En az 3000 karakter olsun. HTML Raw etiketinin içerisine gelicek senin çıktın. Buna uygun formatta çıktı ver.",
                input = prompt
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/responses", requestData);
            
            if(response.IsSuccessStatusCode)
            {
                var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                var outputText = doc.RootElement
                    .GetProperty("output")
                    .EnumerateArray()
                    .First(x =>
                        x.GetProperty("type").GetString() == "message")
                    .GetProperty("content")
                    .EnumerateArray()
                    .First(x =>
                        x.GetProperty("type").GetString() == "output_text")
                    .GetProperty("text")
                    .GetString();

                ViewBag.article = outputText ?? "Makale oluşturulamadı.";
            }

            return View();
        }

        public class OpenAIResponse
        {
            public List<Output> output { get; set; }
        }

        public class Output
        {
            public string type { get; set; }
            public List<Content> content { get; set; }
        }

        public class Content
        {
            public string type { get; set; }
            public string text { get; set; }
        }
    }
}
