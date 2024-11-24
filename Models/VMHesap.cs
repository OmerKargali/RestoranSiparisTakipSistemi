namespace RestoranSiparisTakipSistemi.Models
{
    public class VMHesap
    {
        public Kullanicilar? entity_Kullanicilar { get; set; }
    }

    public class VMKayit
    {
        public int KullaniciId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Sifre { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public string? Rol { get; set; } = "Kullanici";
    }

    public class VMGiris
    {
        public string? Eposta { get; set; }
        public string? Sifre { get; set; }
    }
}