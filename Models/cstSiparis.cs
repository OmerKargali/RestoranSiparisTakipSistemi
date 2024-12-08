public class cstSiparis
{
    public int SiparisDetaylariId { get; set; }
    public int SiparisId { get; set; }
    public int UrunId { get; set; }
    public int KullaniciId { get; set; }
    public string? UrunAdi { get; set; }
    public int Miktar { get; set; } = 1;
    public string? UrunGorseliUrl { get; set; }
    public double BirimFiyat { get; set; }
}