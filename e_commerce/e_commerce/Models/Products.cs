using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models;

public class Products
{
    [Key]
    [Display(Name = "Ürün Kodu")]
    public int ProductId { get; set; }
    
    [Display(Name = "Ürün Adı")]
    public string? ProductName { get; set; } = string.Empty;
    [Display(Name = "İndirimsiz Fiyat")]
    public int? ProductPrice1 { get; set; }
    [Display(Name = "Ürün Açıklaması")]
    public string? ProductDescription { get; set; } = string.Empty;
    [Display(Name = "Ürün Fotoğrafı")]
    public string? ProductPicture { get; set; } = string.Empty;
    [Display(Name = "Ürün Fiyatı")]
    public int? ProductPrice { get; set; }
    [Display(Name = "Ürün Kategorisi")]
    public int? CategoryId { get; set; }
    [Display(Name = "Ürün Kategorisi")]
    virtual public Category? Category { get; set; }
    [NotMapped]
    public IFormFile? ImageUpload { get; set; }

}
