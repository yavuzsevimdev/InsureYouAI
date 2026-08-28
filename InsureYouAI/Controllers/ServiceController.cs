using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly InsureContext _context;

        public ServiceController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult ServiceList()
        {
            var values = _context.Services.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }

        [HttpGet]
        public IActionResult UpdateService(int id)
        {
            var value = _context.Services.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateService(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }

        public IActionResult DeleteService(int id)
        {
            var value = _context.Services.Find(id);
            _context.Services.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }


        public async Task<IActionResult> CreateServiceWithGoogleGemini()
        {
            var apiKey = "GEMINI API KEY";
            var endPoint = "https://generativelanguage.googleapis.com/v1beta/interactions";
            string prompt = "Bir sigorta şirketi için hizmetler bölümü hazırlamanı istiyorum. Burada 5 farklı hizmet olmalı. Bana maksimum 100 karakterden oluşan cümlelerle 5 hizmet içeriği yazar mısın?";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

            var requestData = new
            {
                model = "gemini-3.6-flash",
                input = prompt
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endPoint, content);
            if(!response.IsSuccessStatusCode)
            {
                ViewBag.services = $"Gemini API'den cevap alınamadı. Hata: {response.StatusCode}";
                return View();
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseJson);

            var serviceText = jsonDoc.RootElement
                .GetProperty("steps")[1]
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            var services = serviceText.Split("**")
                          .Where(x => !string.IsNullOrWhiteSpace(x))
                          .Select(x => x.TrimStart('1', '2', '3', '4', '5', '.', ' '))
                          .ToList();

            ViewBag.services = services;
            return View();
        }
    }
}
