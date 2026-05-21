## ⚠️ ÖNEMLİ: Veritabanı Altyapısı Hakkında
Bu projede, SQL Server LocalDB servis kilitlenmelerini, dosya yolu (`AttachDbFilename`) uyuşmazlıklarını ve farklı bilgisayarlarda yaşanabilecek kurulum/bağlantı hatalarını tamamen ortadan kaldırmak amacıyla **Bellek İçi (In-Memory Data Architecture) Sanal Veritabanı** mimarisi kullanılmıştır.

- **Fiziksel Dosya Bağımsızlığı:** Proje klasöründe harici bir `.mdf` veritabanı dosyası bulunmamaktadır.
- **Nasıl Çalışır?:** Uygulama başlatıldığı anda C# (`Form1.cs`) arka planda `USERS`, `MOOD_RECORDS` ve `NOTIFICATIONS` ilişkisel tablo yapılarını doğrudan sistem belleğinde (RAM) dinamik olarak ayağa kaldırır.
- **Hoca / Test Kolaylığı:** Proje, indirildiği her bilgisayarda hiçbir ek SQL servisi yapılandırmasına veya veritabanı ekleme işlemine gerek kalmadan **tek tıkla (Start/Başlat yapılarak) sıfır hatayla** çalışmaya hazırdır.
- # Mood Tracker System (Günlük Ruh Hali Takip Sistemi)

Bu proje, kullanıcıların günlük ruh hallerini kaydedebileceği, geçmişteki duygusal durumlarını listeleyebileceği ve kişisel farkındalıklarını artırabileceği bir **Mood Tracker System** uygulamasıdır. Kullanıcıların mod değişimlerine göre sistem tarafından dinamik olarak destekleyici öneriler ve hatırlatmalar sunulmaktadır.
 

## 🚀 Projenin Amacı ve Çözdüğü Problem
Mood Tracker System, insanların günlük duygu değişimlerini daha rahat görmelerini, kendi ruh hali trendlerini takip etmelerini ve kişisel farkındalık kazanmalarını sağlar. Hızlı ve kullanıcı dostu arayüzü sayesinde veri girişini kolaylaştırır.

## 🛠️ Teknik Mimari ve Teknolojiler
- **Geliştirme Ortamı:** Visual Studio 2022 / C# (WinForms)
- **Arayüz Yönetimi:** Sekmeler arası hızlı geçiş için `TabControl` mimarisi
- **Sistem Mantığı:** Bellek içi (In-Memory Data Architecture) sanal veritabanı altyapısı ile kilitlenme ve bağlantı hatalarından arındırılmış yüksek performanslı veri yönetimi.
- **Algoritmalar:** Kullanıcının seçtiği ruh haline göre anlık analiz yapan dinamik destekleyici öneri mekanizması.

## 📊 Veritabanı Tablo Yapıları (Şema)
Proje arka planda mantıksal olarak şu ilişkisel tablolardan oluşmaktadır:
1. **USERS (Kullanıcılar):** `user_id (PK)`, `ad`, `yaş`, `email`
2. **MOOD_RECORDS (Ruh Hali Kayıtları):** `record_id (PK)`, `user_id (FK)`, `tarih`, `ruh_hali`, `not`
3. **NOTIFICATIONS (Hatırlatma ve Öneriler):** `notification_id (PK)`, `user_id (FK)`, `tarih`, `mesaj`

## 💻 Uygulama Kullanımı ve Ekran Görüntüleri

### 1. Dashboard (Ana Panel)
Uygulama açıldığında kullanıcının son girilen ruh halini ve sistemin en son ürettiği kişisel öneriyi özet kart şeklinde gösterir.
 <img width="893" height="662" alt="Ekran görüntüsü 2026-05-21 235608" src="https://github.com/user-attachments/assets/962c5083-b796-41cf-be2d-fd3b2fbeafdb" />


### 2. Mood Entry (Ruh Hali Girişi)
Kullanıcının o günkü ruh halini seçip (Harika, Mutlu, Normal, Stresli, Üzgün) isteğe bağlı olarak günlük notunu eklediği ve analiz butonuna bastığı alandır.
<img width="894" height="654" alt="Ekran görüntüsü 2026-05-21 235512" src="https://github.com/user-attachments/assets/b234f72d-13e5-4227-b8cd-21f8dfd6d953" />


### 3. History (Geçmiş Kayıtlar)
Kullanıcının geçmişe dönük tüm ruh hali kayıtlarını ve sistem önerilerini tarih bazlı olarak en yeniden en eskiye doğru listelediği tablo alanıdır.
<img width="915" height="658" alt="Ekran görüntüsü 2026-05-21 235525" src="https://github.com/user-attachments/assets/620c9939-5dde-4d32-9a67-5e679a98f42f" /> 



