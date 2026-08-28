using InsureYouAI.Context;
using InsureYouAI.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InsureYouAI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly InsureContext _context;

        public DefaultController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;

            _context.Messages.Add(message);
            _context.SaveChanges();

            #region OpenAI_Analiz
            string apiKey = "OPENAI API KEY";
            string prompt = "" +
                "Sen, InsureYou AI adlı kurumsal bir sigorta şirketinin müşteri iletişim asistanısın.\r\n\r\nGörevin; müşterilerden gelen kasko, trafik, sağlık, konut, DASK, hayat, seyahat sigortası, hasar, poliçe, ödeme, yenileme, iptal, teklif ve şikayet gibi tüm mesajlara şirket adına uygun yanıtlar oluşturmaktır.\r\n\r\nYanıtların profesyonel, samimi, net ve çözüm odaklı olsun. Müşterinin sorununu anladığını göster ve gerekli durumlarda yapılması gereken işlemleri açıkça belirt.\r\n\r\nKesin olarak bilmediğin poliçe kapsamı, fiyat, ödeme, hasar sonucu veya işlem sonucu hakkında tahminde bulunma ve garanti verme. Gerekli durumlarda ilgili birim tarafından inceleme yapılması gerektiğini belirt.\r\n\r\nŞikayet veya olumsuz mesajlarda sakin, anlayışlı ve çözüm odaklı bir dil kullan. Müşterinin kaba veya agresif üslubuna aynı şekilde karşılık verme.\r\n\r\nYanıtına uygun şekilde \"Merhaba,\" ile başla ve profesyonel bir şekilde tamamla. Cevabın yalnızca müşteriye gönderilecek e-posta metni olsun. Açıklama, analiz veya ek yorum ekleme.\r\n\r\nYanıtı \"İyi günler dileriz.\r\nInsureYou AI\" şeklinde sonlandır.";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new{role = "system", content =  prompt},
                    new{role = "user", content = message.MessageDetail}
                },
                max_tokens = 1000,
                temperature = 0.5
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);
            var responseString = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(responseString);
            var textContent = doc.RootElement
                            .GetProperty("choices")[0]
                            .GetProperty("message")
                            .GetProperty("content")
                            .GetString();

            #endregion

            #region Email_Gönderme

            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("InsureYouAI Admin", "yavuzsevimedu@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", message.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = textContent;

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = message.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("yavuzsevimedu@gmail.com", "butd pjna xwbm ftnf");
            client.Send(mimeMessage);
            client.Disconnect(true);
            #endregion

            #region OpenAIMessage_DbKayıt
            OpenAIMessage openAIMessage = new OpenAIMessage()
            {
                MessageDetail = textContent,
                ReceiveEmail = message.Email,
                ReceiveNameSurname = message.NameSurname,
                SendDate = DateTime.Now
            };
            _context.OpenAIMessages.Add(openAIMessage);
            _context.SaveChanges();
            #endregion

            return RedirectToAction("Index");
        }

        public PartialViewResult SubscribeEmail()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult SubscribeEmail(string email)
        {
            return View();
        }
    }
}
