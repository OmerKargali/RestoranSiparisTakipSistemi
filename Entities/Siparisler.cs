using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Siparisler
{
    [Key]
    public int SiparisId { get; set; }

    [ForeignKey("KullaniciId")]
    public int KullaniciId { get; set; }
    public int UrunId { get; set; }
    public DateTime Tarih { get; set; }
    public string? Durum { get; set; }
    public double ToplamTutar { get; set; }
    public string? TahminiTeslimat { get; set; }
    public string? TeslimatAdresi { get; set; }
    // public Urunler? Siparisler_Urunler_FK { get; set; }
    // public Kullanicilar? Siparisler_Kullanicilar_FK { get; set; }

}