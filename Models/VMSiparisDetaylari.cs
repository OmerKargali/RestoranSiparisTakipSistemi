namespace RestoranSiparisTakipSistemi.Models
{
    public class VMSiparisDetaylari
    {
        public int SiparisDetaylariId { get; set; }
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public int Miktar { get; set; }
        public double BirimFiyat { get; set; }
        public double ToplamFiyat { get; set; }
        public SiparisDetaylari? entity_SiparisDetaylari { get; set; }
        public List<SiparisDetaylari>? list_SiparisDetaylari { get; set; }
    }
}