using System.ComponentModel.DataAnnotations;

public class Siparisler
{
    [Key]
    public int SiparisId { get; set; }
    public int MusteriId { get; set; }
    public DateTime Tarih { get; set; }
    public string? Durum { get; set; }
    public double ToplamTutar { get; set; }
    public string? TahminiTeslimat { get; set; }
    public string? TeslimatAdresi { get; set; }
}