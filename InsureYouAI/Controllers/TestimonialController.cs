using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly InsureContext _context;

        public TestimonialController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial service)
        {
            _context.Testimonials.Add(service);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateTestimonial(Testimonial service)
        {
            _context.Testimonials.Update(service);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        public IActionResult DeleteTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            _context.Testimonials.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        public async Task<IActionResult> CreateTestimonialWithGoogleGemini()
        {
            var apiKey = "GEMINI API KEY";
            var endPoint = "https://generativelanguage.googleapis.com/v1beta/interactions";
            string prompt = "Bir sigorta şirketi için müşteri deneyimlerine dair yorum oluşturmak istiyorum. Yani ingilizce karşılığı ile : testimonial. Bu alanda Türkçe olarak 6 tane yorum, 6 tane müşteri adı ve soyadı, bu müşterilerin unvanları olsun. Buna göre içeriği hazırla.";

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
                ViewBag.testimonials = $"Gemini API'den cevap alınamadı. Hata: {response.StatusCode}";
                return View();
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseJson);

            var fullText = jsonDoc.RootElement
                .GetProperty("steps")[1]
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            var testimonials = fullText.Replace("*", "").Split('\n')
                                 .Where(x => !string.IsNullOrEmpty(x))
                                 .Select(x => x.TrimStart('1', '2', '3', '4', '5', '.', ' '))
                                 .ToList();

            ViewBag.testimonials = testimonials;
            return View();

        }
    }
}
