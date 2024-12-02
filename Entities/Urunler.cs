using System.ComponentModel.DataAnnotations;

public class Urunler
{
    [Key]
    public int UrunId { get; set; }
    public int KategoriId { get; set; }
    public string? UrunAdi { get; set; }
    public double Fiyat { get; set; }
    public string? Aciklama { get; set; }
    public string? UrunGorseliUrl { get; set; }
}