using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Siparisler
{
    [Key]
    public int SiparisId { get; set; }

    [ForeignKey("KullaniciId")]
    public int KullaniciId { get; set; }
    public int UrunId { get; set; }
    public string? Durum { get; set; }
    public int Miktar { get; set; } = 1;
    // public Urunler? Siparisler_Urunler_FK { get; set; }
    // public Kullanicilar? Siparisler_Kullanicilar_FK { get; set; }

}