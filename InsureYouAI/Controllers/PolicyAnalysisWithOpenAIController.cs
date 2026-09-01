using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;
using static InsureYouAI.Controllers.ArticleController;

namespace InsureYouAI.Controllers
{
    public class PolicyAnalysisWithOpenAIController : Controller
    {
        private readonly string apiKey = "";

        [HttpGet]
        public IActionResult PdfAnalyze()
        {
            ViewBag.ControllerName = "OpenAI";
            ViewBag.PageName = "OpenAI ile Pdf Analizi";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PdfAnalyze(IFormFile pdfFile)
        {
            ViewBag.ControllerName = "OpenAI";
            ViewBag.PageName = "OpenAI ile Pdf Analizi";

            if (pdfFile == null || pdfFile.Length == 0)
            {
                ViewBag.Error = "Lütfen bir PDF poliçe dosyası yükleyiniz.";
                return View();
            }

            // 1) PDF METNİ ÇIKAR
            string extractedText = await ExtractTextFromPdf(pdfFile);

            if (string.IsNullOrWhiteSpace(extractedText))
            {
                ViewBag.Error = "PDF içerisinden metin çıkarılamadı.";
                return View();
            }

            // 2) OPENAI'A ANALİZ ETTİR
            string analysis = await AnalyzePolicyWithClaude(extractedText);

            ViewBag.OriginalText = extractedText;
            ViewBag.AnalysisResult = analysis;

            return View();
        }

        private async Task<string> ExtractTextFromPdf(IFormFile pdfFile)
        {
            using var ms = new MemoryStream();
            await pdfFile.CopyToAsync(ms);
            ms.Position = 0;

            var sb = new StringBuilder();

            using (var document = PdfDocument.Open(ms))
            {
                foreach (var page in document.GetPages())
                {
                    sb.AppendLine(page.Text);
                    sb.AppendLine("\n");
                }
            }
            return sb.ToString();
        }

        private async Task<string> AnalyzePolicyWithClaude(string policyText)
        {
            var apiUrl = "https://api.openai.com/v1/responses";

            var prompt = $@"
Aşağıdaki metin bir sigorta poliçesine aittir.

Görevlerin:
1) Poliçeyi 10 maddede özetle.
2) Neleri kapsar? (Madde madde yaz)
3) Neleri kapsamaz? (Madde madde yaz)
4) Müşteri için kritik uyarıları **kalın** yap.
5) Yanıtı markdown formatında üret.

--- POLİÇE METNİ ---
{policyText}
--- SON ---
";

            var body = new
            {
                model = "gpt-4o-mini",
                input = prompt,
                max_output_tokens = 4096,
                temperature = 0.2
            };

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(apiUrl, content);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"AI Analizi sırasında hata oluştu: {response.StatusCode}\n{responseText}";
            }

            using var doc = JsonDocument.Parse(responseText);
            var root = doc.RootElement;

            var output = doc.RootElement
         .GetProperty("output");

            var resultSb = new StringBuilder();

            foreach (var item in output.EnumerateArray())
            {
                if (item.GetProperty("type").GetString() == "message")
                {
                    var contentArray = item.GetProperty("content");

                    foreach (var contentItem in contentArray.EnumerateArray())
                    {
                        if (contentItem.GetProperty("type").GetString() == "output_text")
                        {
                            var text = contentItem
                                .GetProperty("text")
                                .GetString();

                            if (!string.IsNullOrEmpty(text))
                            {
                                resultSb.AppendLine(text);
                            }
                        }
                    }
                }
            }

            return resultSb.ToString();
        }
    }
}

