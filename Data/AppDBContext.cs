using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
{
    public DbSet<Kategoriler> Kategoriler { get; set; }
    public DbSet<Kullanicilar> Kullanicilar { get; set; }
    public DbSet<SiparisDetaylari> SiparisDetaylari { get; set; }
    public DbSet<Siparisler> Siparisler { get; set; }
    public DbSet<Urunler> Urunler { get; set; }
    public DbSet<OnaylananSiparis> OnaylananSiparis { get; set; }
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

}