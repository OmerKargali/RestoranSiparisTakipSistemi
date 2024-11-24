namespace RestoranSiparisTakipSistemi.Models
{
    public class VMKategoriler
    {
        public int KategoriId { get; set; }
        public string? KategoriAdi { get; set; }
        public Kategoriler? entity_Kategoriler { get; set; }
        public List<Kategoriler>? list_Kategoriler { get; set; }
    }
}