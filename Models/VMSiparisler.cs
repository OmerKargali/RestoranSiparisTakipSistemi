namespace RestoranSiparisTakipSistemi.Models
{
    public class VMSiparisler
    {
        public int SiparisId { get; set; }
        public int KullaniciId { get; set; }
        public int UrunId { get; set; }
        public DateTime Tarih { get; set; }
        public string? Durum { get; set; }
        public double ToplamTutar { get; set; }
        public string? TahminiTeslimat { get; set; }
        public string? TeslimatAdresi { get; set; }
        public Siparisler? entity_Siparisler { get; set; }
        public List<Siparisler>? list_Siparisler { get; set; }
    }
}