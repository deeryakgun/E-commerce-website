using System;

using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Display(Name = "Kategori Adı")]
    [Required(ErrorMessage ="Bu alan boş bırakılamaz.")]
    public string? CategoryName { get; set; }
    virtual public List<Products>? Products { get; set; }
}

