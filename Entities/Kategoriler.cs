using System.ComponentModel.DataAnnotations;

public class Kategoriler
{
    [Key]
    public int KategoriId { get; set; }
    public string? KategoriAdi { get; set; }
}