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
    public string? Telefon { get; set; }
    public string? Adres { get; set; }
    public string? Rol { get; set; } = "Kullanici";
}