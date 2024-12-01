using System.ComponentModel.DataAnnotations;

namespace RestoranSiparisTakipSistemi.Models
{
    public class VMKullanicilar
    {
        public int KullaniciId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }

        [Required(ErrorMessage = "Eposta alanı zorunludur.")]
        public string? Eposta { get; set; }
        public string? Sifre { get; set; }
        public string? Telefon { get; set; } = "Belirtilmedi.";
        public string? Adres { get; set; } = "Belirtilmedi.";
        public string? Rol { get; set; } = "Kullanici";
        public string? ProfilFotoUrl { get; set; }
        public string? Ulke { get; set; } = "Belirtilmedi";
        public Kullanicilar? entity_Kullanicilar { get; set; }
        public List<Kullanicilar>? list_Kullanicilar { get; set; }
    }
}