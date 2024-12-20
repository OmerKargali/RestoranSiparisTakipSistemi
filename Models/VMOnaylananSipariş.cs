public class VMOnaylananSiparis
{
    public int OnaylananSiparisId { get; set; }
    public int SiparisId { get; set; }
    public int KullaniciId { get; set; }
    public string? TeslimatAdresi { get; set; }
    public string? KrediKartiNumarasi { get; set; }
    public DateTime SonKullanmaTarihi { get; set; }
    public string? CvvKodu { get; set; }
    public string? OdemeYontemi { get; set; }
    public string? SiparisNotu { get; set; }
    public DateTime TahminiTeslimatSuresi { get; set; }
    public string? Durum { get; set; }
    public string? BilgiNotu { get; set; }
    public Siparisler? Siparisler { get; set; }
    public List<cstSiparis>? list_cstSiparis { get; set; }
    public Kullanicilar? Kullanicilar { get; set; }
    public List<OnaylananSiparis>? list_OnaylananSiparis { get; set; }

}