using System;
using System.Data;
using System.Windows.Forms;

namespace MoodTrackerSystem
{
    public partial class Form1 : Form
    {
        // SQL Server kilitlenmelerini aşmak için RAM üzerinde çalışan sanal tablolar oluşturuyoruz
        private DataTable dtUsers;
        private DataTable dtMoodRecords;
        private DataTable dtNotifications;

        private int currentUserId = 1;

        public Form1()
        {
            InitializeComponent();
            InitializeVirtualDatabase();
        }

        // Bilgisayardaki SQL servisinden bağımsız, RAM üzerinde çalışan sanal veritabanı kurulumu
        private void InitializeVirtualDatabase()
        {
            // 1. USERS Tablosu Yapısı
            dtUsers = new DataTable("USERS");
            dtUsers.Columns.Add("user_id", typeof(int));
            dtUsers.Columns.Add("ad", typeof(string));
            dtUsers.Columns.Add("yaş", typeof(int));
            dtUsers.Columns.Add("email", typeof(string));

            // Test Kullanıcısı Ekleme
            dtUsers.Rows.Add(currentUserId, "Test Kullanıcısı", 20, "test@email.com");

            // 2. MOOD RECORDS Tablosu Yapısı
            dtMoodRecords = new DataTable("MOOD_RECORDS");
            dtMoodRecords.Columns.Add("record_id", typeof(int));
            dtMoodRecords.Columns.Add("user_id", typeof(int));
            dtMoodRecords.Columns.Add("tarih", typeof(DateTime));
            dtMoodRecords.Columns.Add("ruh_hali", typeof(string));
            dtMoodRecords.Columns.Add("not", typeof(string));

            // 3. NOTIFICATIONS Tablosu Yapısı
            dtNotifications = new DataTable("NOTIFICATIONS");
            dtNotifications.Columns.Add("notification_id", typeof(int));
            dtNotifications.Columns.Add("user_id", typeof(int));
            dtNotifications.Columns.Add("tarih", typeof(DateTime));
            dtNotifications.Columns.Add("mesaj", typeof(string));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHistory();
            UpdateDashboard();
        }

        // 1. KAYDET VE ÖNERİ AL BUTONU
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbMood.SelectedItem == null)
            {
                MessageBox.Show("Lütfen önce bir ruh hali seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedMood = cmbMood.SelectedItem.ToString();
            string note = txtNote.Text;
            DateTime now = DateTime.Now;

         //   [cite_start]// Ödev Gereksinimi: Ruh haline göre dinamik destekleyici öneri belirleme algoritması [cite: 18, 22]
            string suggestion = "";
            switch (selectedMood)
            {
                case "Harika":
                    suggestion = "Harika bir gün! Bu enerjiyi sevdiklerinle paylaşmaya ne dersin?";
                    break;
                case "Mutlu":
                    suggestion = "Yüzündeki tebessümü koru. Bugün seni mutlu eden şeyleri bir kenara not et!";
                    break;
                case "Normal":
                    suggestion = "Dengeli bir gün. Kısa bir yürüyüş ya da sevdiğin bir kahve günü daha da güzelleştirebilir.";
                    break;
                case "Stresli":
                    suggestion = "Derin bir nefes al. 5 dakika gözlerini kapat ve sadece nefesine odaklan. Geçecek.";
                    break;
                case "Üzgün":
                    suggestion = "Kendine yüklenme, her duygu insana dair. En sevdiğin müziği açıp biraz dinlenmeye ne dersin?";
                    break;
                default:
                    suggestion = "Kendine zaman ayırmayı ve nazik davranmayı unutma.";
                    break;
            }

            // Sanal Veritabanına Kayıt Ekleme (SQL injection ve kilitlenme riski sıfır)
            int nextRecordId = dtMoodRecords.Rows.Count + 1;
            dtMoodRecords.Rows.Add(nextRecordId, currentUserId, now, selectedMood, string.IsNullOrEmpty(note) ? "" : note);

            int nextNotifyId = dtNotifications.Rows.Count + 1;
            dtNotifications.Rows.Add(nextNotifyId, currentUserId, now, "Öneri: " + suggestion);

            //[cite_start]// Arayüzü güncelle ve kullanıcıya göster [cite: 18, 22]
            lblTodayMood.Text = "Bugünkü Ruh Haliniz: " + selectedMood;
            lblSuggestion.Text = "Sistem Önerisi: " + suggestion;

            MessageBox.Show("Ruh haliniz başarıyla kaydedildi ve analiz edildi!\n\nSistem Önerisi:\n" + suggestion, "Durum Analizi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Temizlik ve Ekranları Yenileme
            txtNote.Clear();
            cmbMood.SelectedIndex = -1;
            LoadHistory();
            UpdateDashboard();
        }

       // [cite_start]// 2. GEÇMİŞ KAYITLARI LİSTELEME FONKSİYONU [cite: 19]
        private void LoadHistory()
        {
            // DataGridView için gösterim tablosu oluşturuyoruz
            DataTable displayTable = new DataTable();
            displayTable.Columns.Add("Kayıt Tarihi", typeof(DateTime));
            displayTable.Columns.Add("Ruh Hali", typeof(string));
            displayTable.Columns.Add("Kullanıcı Notu", typeof(string));

            // Kayıtları tersten (en yeni üste gelecek şekilde) listeliyoruz
            for (int i = dtMoodRecords.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dtMoodRecords.Rows[i];
                displayTable.Rows.Add(row["tarih"], row["ruh_hali"], row["not"]);
            }

            dgvHistory.DataSource = displayTable;
        }

       // [cite_start]// 3. ANA PANELİ GÜNCELLEME FONKSİYONU [cite: 33]
        private void UpdateDashboard()
        {
            if (dtMoodRecords.Rows.Count > 0)
            {
                DataRow lastMood = dtMoodRecords.Rows[dtMoodRecords.Rows.Count - 1];
                lblTodayMood.Text = "Son Girilen Ruh Haliniz: " + lastMood["ruh_hali"].ToString();
            }

            if (dtNotifications.Rows.Count > 0)
            {
                DataRow lastNotify = dtNotifications.Rows[dtNotifications.Rows.Count - 1];
                string msg = lastNotify["mesaj"].ToString().Replace("Öneri: ", "");
                lblSuggestion.Text = "Sistem Önerisi: " + msg;
            }
        }
    }
}