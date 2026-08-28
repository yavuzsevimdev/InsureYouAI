using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace InsureYouAI.Controllers
{
    public class ImageAIController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageAIController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult CreateImageWithOpenAI()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageWithOpenAI(string prompt)
        {
            var apiKey = "OPENAI API KEY";
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-image-1.5",
                prompt = prompt,
                n = 1,
                size = "1024x1024",
                quality = "low",
                output_format = "png"
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "OpenAI Hatası: " + json;
                return View();
            }

            var result = JsonDocument.Parse(json);
            var imageBase64 = result.RootElement
                .GetProperty("data")[0] 
                .GetProperty("b64_json")
                .GetString();

            var imageBytes = Convert.FromBase64String(imageBase64);

            var fileName = Guid.NewGuid() + ".png";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "ai",
                fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            var imageUrl = "/images/ai/" + fileName;

            return View(model: imageUrl);
        }
    }
}
