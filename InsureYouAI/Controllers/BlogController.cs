using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class BlogController : Controller
    {
        private readonly InsureContext _context;

        public BlogController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult BlogList()
        {
            return View();
        }

        public IActionResult GetBlogByCategory(int id)
        {
            ViewBag.c = id;
            return View();
        }

        public PartialViewResult GetBlog()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult GetBlog(string keyword)
        {
            return View();
        }

        public IActionResult BlogDetail(int id)
        {
            ViewBag.i = id;
            TempData["id"] = id;
            return View();
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.AppUserId = "6615939e-af4d-40f0-99d0-fb7f00a1507c";

            using (var client = new HttpClient())
            {
                var apiKey = "OPENAI API KEY";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                try
                {
                    var requestData = new
                    {
                        model = "gpt-4o",
                        instructions = @"Sen bir yorum toksiklik analiz sistemisin.

                        Verilen yorumu analiz et ve toksiklik seviyesini 0 ile 1 arasında değerlendir.

                        0 = Hiç toksik değil
                        0.25 = Hafif olumsuz
                        0.50 = Orta derecede toksik
                        0.75 = Toksik
                        1.00 = Aşırı toksik

                        Ayrıca yorumu şu kategorilerden birine ayır:

                        Normal
                        Olumsuz
                        Toksik
                        Aşırı Toksik

                        Sonucu aşağıdaki JSON formatında döndür:

                        {
                            ""status"": ""Toksik"",
                            ""score"": 0.85
                        }",
                        input = comment.CommentDetail
                    };

                    var json = JsonSerializer.Serialize(requestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://api.openai.com/v1/responses", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(responseString);

                    var outputText = doc.RootElement
                        .GetProperty("output")
                        .EnumerateArray()
                        .First(x => x.GetProperty("type").GetString() == "message")
                        .GetProperty("content")
                        .EnumerateArray()
                        .First(x => x.GetProperty("type").GetString() == "output_text")
                        .GetProperty("text")
                        .GetString();

                    outputText = outputText
                        .Replace("```json", "")
                        .Replace("```", "")
                        .Trim();

                    var toxicityDoc = JsonDocument.Parse(outputText);

                    var status = toxicityDoc.RootElement
                        .GetProperty("status")
                        .GetString();

                    var score = toxicityDoc.RootElement
                        .GetProperty("score")
                        .GetDouble();

                    if (score >= 0.5)
                    {
                        comment.CommentStatus = "Toksik Yorum";
                    }
                    else
                    {
                        comment.CommentStatus = "Yorum Onaylandı";
                    }

                }
                catch (Exception ex)
                {
                    comment.CommentStatus = "Onay Bekliyor";
                }
            }


            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("BlogList");
        }
    }
}