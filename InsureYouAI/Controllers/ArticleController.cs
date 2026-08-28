using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
            var values = _context.Articles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticleWithOpenAI(string prompt)
        {
            var apiKey = "OPENAI API KEY";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-5.6",
                instructions = "Sen bir sigorta şirketi için çalışan, içerik yazarlığı yapan bir yapay zekasın. Kullanıcının verdiği özet ve anahtar kelimelere göre sigortacılık sektörüyle ilgili makale üret. En az 3000 karakter olsun.",
                input = prompt
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/responses", requestData);
            
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                var content = result.output[1].content[0].text;
                ViewBag.article = content ?? "Makale oluşturulamadı.";
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
