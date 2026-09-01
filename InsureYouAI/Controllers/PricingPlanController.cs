using InsureYouAI.Context;
using InsureYouAI.Entities;
using InsureYouAI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace InsureYouAI.Controllers
{
    public class PricingPlanController : Controller
    {
        private readonly InsureContext _context;

        public PricingPlanController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult PricingPlanList()
        {
            ViewBag.ControllerName = "Ödeme Planları";
            ViewBag.PageName = "Mevcut Ödeme Planları";

            var values = _context.PricingPlans.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreatePricingPlan()
        {
            ViewBag.ControllerName = "Ödeme Planları";
            ViewBag.PageName = "Yeni Ödeme Planı Ekle";

            return View();
        }

        [HttpPost]
        public IActionResult CreatePricingPlan(PricingPlan pricingPlan)
        {
            _context.PricingPlans.Add(pricingPlan);
            _context.SaveChanges();
            return RedirectToAction("PricingPlanList");
        }

        [HttpGet]
        public IActionResult UpdatePricingPlan(int id)
        {
            ViewBag.ControllerName = "Ödeme Planları";
            ViewBag.PageName = "Mevcut Ödeme Planı Güncelleme Sayfası";

            var value = _context.PricingPlans.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdatePricingPlan(PricingPlan pricingPlan)
        {
            _context.PricingPlans.Update(pricingPlan);
            _context.SaveChanges();
            return RedirectToAction("PricingPlanList");
        }

        public IActionResult DeletePricingPlan(int id)
        {
            var value = _context.PricingPlans.Find(id);
            _context.PricingPlans.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("PricingPlanList");
        }

        public IActionResult ChangeStatus(int id)
        {
            var value = _context.PricingPlans.Find(id);

            value.IsFeature = (value.IsFeature == true) ?  false :  true;

            _context.SaveChanges();

            return RedirectToAction("PricingPlanList");
        }

        [HttpGet]
        public IActionResult CreateUserCustomizePlan()
        {
            ViewBag.ControllerName = "AI Destekli Sigorta Planı";
            ViewBag.PageName = "Kullanıcıya Özel AI Destekli Sigorta Planı Belirleme";

            var model = new AIInsuranceRecommendationViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserCustomizePlan(AIInsuranceRecommendationViewModel model)
        {
            ViewBag.ControllerName = "AI Destekli Sigorta Planı";
            ViewBag.PageName = "Kullanıcıya Özel AI Destekli Sigorta Planı Belirleme";

            var apiKey = "";
            
            // Kullanıcı girdilerini JSON'a çeviriyoruz
            var userJson = JsonConvert.SerializeObject(model);

            // OpenAI'ye göndereceğimiz prompt:
            var prompt = $@"
Sen profesyonel bir sigorta uzmanı AI asistanısın. 
Aşağıdaki kullanıcının bilgilerini analiz ederek en uygun sigorta paketini öner.

Paketler ve özellikleri:
1) Premium Paket (599 TL/ay): Yatarak tedavi, check-up, geniş yol yardım, yurtiçi seyahat güvencesi.
2) Standart Paket (449 TL/ay): Acil sağlık, müşteri hizmetleri, kaza sonrası tıbbi destek.
3) Ekonomik Paket (339 TL/ay): Temel sağlık, temel yol yardım.

Kullanıcı bilgileri:
{userJson}

Sadece şu formatta JSON döndür:

{{
  ""RecommendedPackage"": ""Premium | Standart | Ekonomik"",
  ""SecondBestPackage"": ""Premium | Standart | Ekonomik"",
  ""AnalysisText"": ""Kısa analiz metni""
}}
";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var body = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            dynamic ai = JsonConvert.DeserializeObject(jsonResponse);
            string aiResult = ai.choices[0].message.content;

            // AI cevabı JSON formatında gelmiş olacak
            var result = JsonConvert.DeserializeObject<AIInsuranceRecommendationViewModel>(aiResult);

            // Sonuçları modele geri yazıyoruz
            model.RecommendedPackage = result.RecommendedPackage;
            model.SecondBestPackage = result.SecondBestPackage;
            model.AnalysisText = result.AnalysisText;

            return View(model);
        }

    }

}
