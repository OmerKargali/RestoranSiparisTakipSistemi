namespace RestoranSiparisTakipSistemi
{
    public class IndexViewModel
    {
        public Kategoriler? Kategoriler { get; set; }
        public IEnumerable<Kategoriler>? Kategorilers { get; set; }

        public Kullanicilar? Kullanicilar { get; set; }
        public IEnumerable<Kullanicilar>? Kullanicilars { get; set; }

        public SiparisDetaylari? SiparisDetaylari { get; set; }
        public IEnumerable<SiparisDetaylari>? SiparisDetaylaris { get; set; }

        public Siparisler? Siparisler { get; set; }
        public IEnumerable<Siparisler>? Siparislers { get; set; }

        public Urunler? Urunler { get; set; }
        public IEnumerable<Urunler>? Urunlers { get; set; }

        public OnaylananSiparis? OnaylananSiparis { get; set; }
        public IEnumerable<OnaylananSiparis>? OnaylananSiparislers { get; set; }

        public List<cstSiparis>? list_cstSiparis { get; set; }
        public List<cstOnaylananSiparisler>? list_cstOnaylananSiparisler { get; set; }

    }
}