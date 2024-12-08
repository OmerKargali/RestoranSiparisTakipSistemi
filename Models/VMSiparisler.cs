namespace RestoranSiparisTakipSistemi.Models
{
    public class VMSiparisler
    {
        public int SiparisId { get; set; }
        public int KullaniciId { get; set; }
        public int UrunId { get; set; }
        public string? Durum { get; set; }
        public int Miktar { get; set; }
        public Siparisler? entity_Siparisler { get; set; }
        public List<Siparisler>? list_Siparisler { get; set; }
        public ICollection<OnaylananSiparis>? OnaylananSiparis { get; set; }
    }
}