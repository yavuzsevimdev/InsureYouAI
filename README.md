# 🤖 InsureYouAI

### Yapay Zeka Destekli Sigorta Yönetim ve Analiz Platformu

InsureYouAI, **ASP.NET Core** kullanılarak geliştirilen, farklı yapay zeka ve makine öğrenmesi teknolojilerinin gerçek bir sigorta uygulamasına entegre edilmesini amaçlayan bir web uygulamasıdır.

Bu projenin temel amacı yalnızca klasik bir sigorta yönetim sistemi oluşturmak değil; **yapay zekanın kullanıcı analizi, içerik üretimi, chatbot, poliçe analizi, tahminleme ve web araştırması gibi farklı süreçlerde ASP.NET Core uygulamalarına nasıl entegre edilebileceğini uygulamalı olarak göstermektir.**

---

## 🎯 Projenin Amacı

Günümüzde yapay zeka teknolojileri yazılım projelerinin önemli bir parçası haline gelmektedir.

InsureYouAI projesinde farklı AI servisleri ve makine öğrenmesi teknolojileri kullanılarak bu teknolojilerin gerçek bir sigorta sistemi içerisinde hangi alanlarda kullanılabileceği üzerinde çalışılmıştır.

Proje içerisinde;

- Kullanıcı verilerinin AI ile analiz edilmesi
- Kişiye özel sigorta paketi önerilmesi
- Toksik yorum analizi
- Kullanıcı mesajlarının kategorize edilmesi
- Mesaj önceliklerinin belirlenmesi
- AI tarafından kullanıcı mesajlarına cevap oluşturulması
- Sesli AI chatbot
- AI destekli makale oluşturma
- Makale için özel kapak görselleri oluşturma
- PDF poliçe analizi
- Web üzerinden gerçek zamanlı araştırma
- ML.NET ile satış tahminleri
- Dashboard üzerinde geleceğe yönelik tahminlerin gösterilmesi

gibi farklı yapay zeka senaryoları uygulanmıştır.

---

# 🧠 Yapay Zeka Entegrasyonları

## 🤖 OpenAI

OpenAI entegrasyonu ile projenin birçok farklı bölümünde yapay zeka destekli özellikler geliştirilmiştir.

### Kullanım Alanları

- ✍️ AI destekli makale oluşturma
- 📝 Makale hakkında yazısı oluşturma
- 🖼️ Makaleler için özel kapak görselleri oluşturma
- 📄 Sigorta poliçelerini PDF üzerinden analiz etme
- 🎯 Kullanıcı bilgilerine göre sigorta paketi önerme
- 💬 Kullanıcı mesajlarına AI destekli cevap oluşturma
- 👤 Kullanıcı bilgilerinin analiz edilmesi

---

## ✨ Google Gemini

Google Gemini kullanılarak kullanıcı mesajlarının ve yorumlarının yapay zeka tarafından analiz edilmesi sağlanmıştır.

### Kullanım Alanları

- 💬 Kullanıcı mesajlarını sigortacılık kategorilerine ayırma
- 🚦 Mesajların öncelik seviyelerini belirleme
- 🔎 Kullanıcı içeriklerini analiz etme
- ☣️ Toksik yorumların tespit edilmesi

Örneğin kullanıcı tarafından gönderilen bir mesaj analiz edilerek;

**Kasko, Trafik Sigortası, Sağlık Sigortası, Konut Sigortası, Hasar Bildirimi, Fiyat Teklifi, Poliçe Yenileme, Genel Soru veya İletişim Talebi**

gibi kategorilerden uygun olan belirlenebilmektedir.

---

## 🧠 Anthropic Claude

Anthropic Claude entegrasyonu ile farklı bir yapay zeka sağlayıcısının ASP.NET Core uygulaması içerisinde kullanım senaryoları üzerinde çalışılmıştır.

Bu entegrasyon sayesinde farklı AI sağlayıcılarının API yapıları ve uygulama içerisinde kullanılma yöntemleri deneyimlenmiştir.

---

## 🌐 Tavily

Tavily entegrasyonu sayesinde AI destekli web araştırması gerçekleştirilmiştir.

### Kullanım Alanları

- 🔎 Web üzerinde gerçek zamanlı araştırma
- 🌍 Güncel internet bilgilerinin araştırılması
- 🧠 Web kaynaklarının AI destekli süreçlerde kullanılması

---

## 🔊 ElevenLabs

ElevenLabs entegrasyonu ile yapay zeka tarafından oluşturulan metin cevaplarının sesli yanıta dönüştürülmesi sağlanmıştır.

### Kullanım Alanları

- 🎙️ AI chatbot sesli yanıt
- 🔊 Metinden sese dönüşüm
- 💬 Daha etkileşimli chatbot deneyimi

Bu özellik sayesinde chatbot yalnızca yazılı cevap veren bir sistem olmaktan çıkarılarak **sesli iletişim özelliğine** de sahip hale getirilmiştir.

---

## 📈 ML.NET

ML.NET kullanılarak geçmiş poliçe satış verilerinden yararlanılarak geleceğe yönelik tahminleme çalışmaları yapılmıştır.

### Kullanım Alanları

- 📊 Poliçe satış verilerinin analiz edilmesi
- 📈 Satış tahminleri
- 🔮 Geleceğe yönelik öngörüler
- 📊 Tahmin sonuçlarının Dashboard üzerinde gösterilmesi

---

# 🚀 Temel Özellikler

### 👤 Kullanıcı Yönetimi

- Kullanıcı kayıt ve giriş sistemi
- ASP.NET Core Identity
- Kullanıcı profili
- Kullanıcı yorumları
- Kullanıcı poliçeleri
- Kullanıcı bilgilerinin yönetimi

### 💬 AI Destekli Mesaj Sistemi

Kullanıcı tarafından gönderilen mesajlar AI tarafından analiz edilerek;

- Mesaj kategorisi
- Mesaj önceliği
- Mesaj içeriği

değerlendirilebilmektedir.

AI tarafından oluşturulan cevaplar kullanıcıya iletişim sürecinde yardımcı olacak şekilde kullanılabilmektedir.

---

### ☣️ Toksik Yorum Analizi

Kullanıcıların oluşturduğu yorumlar AI destekli olarak analiz edilerek toksik veya uygunsuz içeriklerin tespit edilmesi sağlanmıştır.

---

### 🎯 AI Destekli Sigorta Paketi Önerisi

Kullanıcının çeşitli bilgileri değerlendirilerek kişiye özel sigorta paketi önerilmektedir.

Analiz sırasında;

- Yaş
- Meslek
- Şehir
- Medeni durum
- Çocuk sayısı
- Seyahat sıklığı
- Aylık bütçe
- Teminat önceliği
- Diğer kullanıcı bilgileri

gibi bilgiler kullanılmaktadır.

AI sonucunda;

**En uygun paket + ikinci alternatif paket + analiz açıklaması**

kullanıcıya sunulmaktadır.

---

### 📄 AI Poliçe Analizi

PDF formatındaki sigorta poliçeleri sistem tarafından okunarak metin içerikleri çıkarılmakta ve AI tarafından analiz edilmektedir.

Analiz sonucunda;

- 📌 Poliçe özeti
- ✅ Kapsanan durumlar
- ❌ Kapsanmayan durumlar
- ⚠️ Kritik uyarılar

kullanıcıya sunulmaktadır.

---

### ✍️ AI Makale Oluşturucu

Admin paneli üzerinden yapay zeka kullanılarak sigortacılık alanında makaleler oluşturulabilmektedir.

- Makale başlığı
- Makale içeriği
- Makale hakkında yazısı
- Makale kapak görseli

AI destekli olarak oluşturulabilmektedir.

---

### 🔎 AI Web Araştırması

Tavily entegrasyonu ile web üzerinde araştırma yapılarak güncel bilgiler AI destekli süreçlerde kullanılabilmektedir.

---

### 🎙️ Sesli AI Chatbot

ElevenLabs entegrasyonu sayesinde AI tarafından oluşturulan cevaplar sese dönüştürülerek kullanıcıya sesli şekilde sunulmaktadır.

---

### 📊 Dashboard

Admin Dashboard üzerinde çeşitli sigorta verileri görselleştirilmiştir.

- Poliçe sayıları
- Poliçe türleri
- Aylık kazançlar
- Aylık giderler
- Kullanıcı bilgileri
- Satış tahminleri
- Geleceğe yönelik tahminler

gibi bilgiler grafikler ve istatistikler halinde gösterilmektedir.

---

###  📸 Ekran Görüntüleri

<img width="1350" height="9193" alt="01-Default-Index" src="https://github.com/user-attachments/assets/b089bc0e-eb67-488c-a55d-4101d358e531" />
<img width="1902" height="944" alt="02-Dashboard-Index-1" src="https://github.com/user-attachments/assets/643de5da-102f-4485-8f44-90c2b0d97530" />
<img width="1903" height="945" alt="03-Dashboard-Index-2" src="https://github.com/user-attachments/assets/5c809813-7702-4166-927b-72d144e8e8e0" />
<img width="1903" height="940" alt="04-Dashboard-Index-3" src="https://github.com/user-attachments/assets/8598ffb1-3f63-442b-98da-a9e4467ae5d8" />
<img width="1350" height="11413" alt="05-Blog-BlogList" src="https://github.com/user-attachments/assets/c9c70a03-9470-4a43-89d0-c54ca0daebc1" />
<img width="1350" height="5952" alt="06-Blog-BlogDetail" src="https://github.com/user-attachments/assets/156b2f1d-ef41-40f2-84c9-9175ded1104f" />
<img width="1905" height="944" alt="07-Article-ArticleList" src="https://github.com/user-attachments/assets/10d774e9-1f17-43e2-b2b2-5cee1a43228b" />
<img width="1350" height="1623" alt="08-Article-CreateArticleWithOpenAI" src="https://github.com/user-attachments/assets/6aca9241-7e9c-4a50-bdeb-bd6e335895cc" />
<img width="1903" height="945" alt="09-AppUser-UserList" src="https://github.com/user-attachments/assets/5cad9cb3-1f49-4a0b-81aa-2bb33e39de10" />
<img width="1904" height="941" alt="10-AppUser-UserProfileWithAI" src="https://github.com/user-attachments/assets/aba2d5cc-dc19-43ac-9587-8d9dbb4b8d23" />
<img width="1919" height="943" alt="11-Chat-Index" src="https://github.com/user-attachments/assets/a59d0cc6-3bc6-4082-b87c-ae851ad487ee" />
<img width="1919" height="945" alt="12-Testimonial-TestimonialList" src="https://github.com/user-attachments/assets/7e1deb23-4168-4edf-b50d-64599b51d5d0" />
<img width="1917" height="944" alt="13-Contact-ContactList" src="https://github.com/user-attachments/assets/e1de5e9f-8950-494c-8743-c78abe6b9744" />
<img width="1919" height="943" alt="14-Message-MessageList" src="https://github.com/user-attachments/assets/1b77e12d-ce03-4116-81d2-0b0f0a61ba04" />
<img width="1919" height="941" alt="15-About-AboutList" src="https://github.com/user-attachments/assets/cfc224f1-810f-4411-96b2-3e8cd4f65fc2" />
<img width="1917" height="942" alt="16-Category-CategoryList" src="https://github.com/user-attachments/assets/e020d00c-a3ca-4221-ba42-f442b04fc69f" />
<img width="1919" height="942" alt="17-Service-ServiceList" src="https://github.com/user-attachments/assets/ab9cc9fc-638e-4cae-83b1-d7cec5cabafe" />
<img width="1919" height="941" alt="18-PricingPlan-PricingPlanList" src="https://github.com/user-attachments/assets/07ba4d34-da7c-482a-8a3d-bfae09ed6898" />

---

# 🛠️ Kullanılan Teknolojiler

## Backend

- C#
- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- LINQ
- REST API
- Dependency Injection
- ViewComponent
- HTTP Client

## Database

- Microsoft SQL Server
- Entity Framework Core
- Code First
- Migrations
- İlişkisel Veritabanı

## Frontend

- HTML5
- CSS3
- Bootstrap
- JavaScript
- Razor
- Bootstrap Icons
- Chart.js
- ApexCharts

## Artificial Intelligence & Machine Learning

- OpenAI
- Google Gemini
- Anthropic Claude
- Tavily
- ElevenLabs
- ML.NET

---

# 🏗️ Proje Yapısı

Projenin ana amacı farklı yapay zeka teknolojilerinin ASP.NET Core uygulamalarına entegrasyonunu gerçekleştirmek olduğu için aşırı katmanlı ve karmaşık bir mimari yerine, AI entegrasyonlarını rahat şekilde geliştirmeye ve yönetmeye uygun bir proje yapısı tercih edilmiştir.

Bununla birlikte kod organizasyonu için controller, entity, service, model, DTO ve ViewComponent yapıları ayrı klasörlerde tutulmuştur.

```text
InsureYouAI
│
├── Context
├── Controllers
├── Dtos
├── Entities
├── Migrations
├── Models
├── Services
├── ViewComponents
│   ├── AdminLayoutViewComponents
│   ├── BlogViewComponents
│   ├── DashboardViewComponents
│   └── DefaultViewComponents
│
├── Views
├── wwwroot
├── appsettings.json
└── Program.cs
