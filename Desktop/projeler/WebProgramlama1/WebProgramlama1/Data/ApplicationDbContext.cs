using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebProgramlama1.Models; // Modelleri görmek için

namespace WebProgramlama1.Data
{
    // Identity entegrasyonu için DbContext yerine IdentityDbContext<ApplicationUser> kullanıyoruz.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        // Users tablosu zaten IdentityDbContext içinde tanımlıdır, 
        // ancak özel sorgular için buraya ekleyebilirsin, opsiyoneldir.
        // public DbSet<ApplicationUser> AppUsers { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Identity ayarları için zorunlu!

            // --- Tablo İsimlerini Türkçeleştirme/Sadeleştirme ---
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // --- İlişki Ayarları (Fluent API) ---

            // Bir Kategori silinirse ürünleri silinmesin (Restrict) veya silinsin (Cascade). 
            // Güvenlik için Restrict önerilir.
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Favori tablosundaki benzersizlik (Aynı kullanıcı aynı ürünü 2 kere favorileyemesin)
            // Composite Key mantığına gerek yok ama index atabiliriz.
        }
    }
}