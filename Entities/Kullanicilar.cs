using System.ComponentModel.DataAnnotations;
public class Kullanicilar
{
    [Key]
    public int KullaniciId { get; set; }
    public string? Ad { get; set; }
    public string? Soyad { get; set; }

    [Required]
    public string? Eposta { get; set; }
    public string? Sifre { get; set; }

    [Required]
    public string? Telefon { get; set; } = "Belirtilmedi";
    public string? Adres { get; set; } = "Belirtilmedi";
    public string? Rol { get; set; } = "Kullanici";
    public string? ProfilFotoUrl { get; set; }
    public string? Ulke { get; set; } = "Belirtilmedi";
}