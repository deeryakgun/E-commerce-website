using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models;

public class Slider
{
    [Key]
    public int SliderId { get; set; }
    [Display(Name = "Slider Adı")]
    public string? SliderName { get; set; } = string.Empty;
    [Display(Name = "Başlık")]
    public string? Header1 { get; set; } = string.Empty;
    [Display(Name = "Alt Başlık")]
    public string? Header2 { get; set; } = string.Empty;
    [Display(Name = "Metin")]
    public string? Context { get; set; } = string.Empty;
    [Display(Name = "Fotoğraf")]
    public string? SliderImage { get; set; } = string.Empty;
    [NotMapped]
    public IFormFile? ImageUpload { get; set; }

}