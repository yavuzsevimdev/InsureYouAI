using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class AboutItemController : Controller
    {
        private readonly InsureContext _context;

        public AboutItemController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult AboutItemList()
        {
            ViewBag.ControllerName = "Hakkımızda Ögeleri";
            ViewBag.PageName = "Mevcut Hakkımızda Ögeleri";

            var values = _context.AboutItems.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAboutItem()
        {
            ViewBag.ControllerName = "Hakkımızda";
            ViewBag.PageName = "Yeni Hakkımızda Öge Girişi";

            return View();
        }

        [HttpPost]
        public IActionResult CreateAboutItem(AboutItem aboutItem)
        {
            _context.AboutItems.Add(aboutItem);
            _context.SaveChanges();
            return RedirectToAction("AboutItemList");
        }

        [HttpGet]
        public IActionResult UpdateAboutItem(int id)
        {
            ViewBag.ControllerName = "Hakkımızda";
            ViewBag.PageName = "Mevcut Hakkımızda Ögeleri Güncelleme Sayfası";

            var value = _context.AboutItems.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAboutItem(AboutItem aboutItem)
        {
            _context.AboutItems.Update(aboutItem);
            _context.SaveChanges();
            return RedirectToAction("AboutItemList");
        }

        public IActionResult DeleteAboutItem(int id)
        {
            var value = _context.AboutItems.Find(id);
            _context.AboutItems.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("AboutItemList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateAboutItemWithGoogleGemini()
        {
            var apiKey = "";
            var endPoint = "https://generativelanguage.googleapis.com/v1beta/interactions";
            var prompt = "Kurumsal bir sigorta firması için etkileyici, güven verici ve profesyonel bir 'Hakkımızda alanları (about item)' yazısı oluştur. Örneğin 'Geleceğinizi güvence altına alan kapsamlı sigorta çözümleri sunuyoruz.' şeklinde ve en fazla bu metin karakter uzunluğu kadar karakter uzunluğu olacak veya bunun gibi ve buna benzer daha zengin içerikler gelsin. 1 tane item istiyorum. HTML sayfasında görünteleyeceğim için çıktıyı HTML.Raw diyerek yazdıracağım. Buna uygun formatta çıktı ver.";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

            var requestData = new
            {
                model = "gemini-3.6-flash",
                input = prompt
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endPoint, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.aboutItemText = $"Gemini API'den cevap alınamadı. Hata: {response.StatusCode}";
                return View();
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseJson);

            var aboutItemText = jsonDoc.RootElement
                .GetProperty("steps")[1]
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            ViewBag.aboutItemText = aboutItemText;
            return View();
        }
    }
}
