using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using e_commerce.Dto;
using e_commerce.Models;

namespace e_commerce.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Slider> Slider { get; set; }
    
    


    

}
