public class cstOnaylananSiparisler
{
    public int OnaylananSiparisId { get; set; }
    public int SiparisId { get; set; }
    public int KullaniciId { get; set; }
    public int UrunId { get; set; }
    public string? UrunAdi { get; set; }
    public string? UrunGorseliUrl { get; set; }
    public string? TeslimatAdresi { get; set; }
    public string? KrediKartiNumarasi { get; set; }
    public DateTime SonKullanmaTarihi { get; set; }
    public string? CvvKodu { get; set; }
    public string? OdemeYontemi { get; set; }
    public string? SiparisNotu { get; set; }
    public DateTime TahminiTeslimatSuresi { get; set; }
    public string? Durum { get; set; }
}