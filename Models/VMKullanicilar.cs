namespace RestoranSiparisTakipSistemi.Models
{
    public class VMKullanicilar
    {
        public int KullaniciId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Sifre { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public string? Rol { get; set; } = "Kullanici";
        public Kullanicilar? enetity_Kullanicilar { get; set; }
        public List<Kullanicilar>? list_Kullanicilar { get; set; }
    }
}