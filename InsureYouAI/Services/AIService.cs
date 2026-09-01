using Microsoft.Identity.Client;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Services
{
    public class AIService
    {
        private readonly string _apiKey = "";
        private readonly string _model = "gemini-3.6-flash";

        public async Task<string> PredictCategoryAsync(string messageText)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/interactions";

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("x-goog-api-key", _apiKey);

            var requestData = new
            {
                model = _model,
                input = $"Aşağıdaki kullanıcı mesajını sigortacılık alanında kategorize et. Sadece kategori adı döndür.\n\nMesaj: {messageText}\n\nOlası kategoriler:\n- Kasko\n- Trafik Sigortası\n- Sağlık Sigortası\n- Konut Sigortası\n- Hasar Bildirimi\n- Fiyat Teklifi\n- Poliçe Yenileme\n- Genel Soru\n- İletişim Talebi\n"
            };


            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var text = doc.RootElement
                .GetProperty("steps")[1]
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            return text.Trim();
        }

        public async Task<string> PredictPriorityAsync(string messageText)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/interactions";


            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("x-goog-api-key", _apiKey);

            var requestData = new
            {
                model = _model,
                input = $@"
Aşağıdaki kullanıcı mesajının aciliyet seviyesini belirle.
Sadece 3 seçenekten birini döndür: High, Medium, Low.

Kurallar:
- Kaza, hasar, ödeme sorunları, acil durumlar → High
- Fiyat teklifi, yenileme, teminat soruları → Medium
- Genel sorular, merak edilen bilgiler → Low

Mesaj:
{messageText}"
            };


            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var text = doc.RootElement
                .GetProperty("steps")[1]
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            return text.Trim();
        }
    }
}
