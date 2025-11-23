using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebProgramlama1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50)]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50)]
        public string Soyad { get; set; }

        public string? ProfilResmiUrl { get; set; }
        
        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        // Navigation Properties (İlişkiler)
        public virtual ICollection<Product> Products { get; set; } // Sattığı ürünler
        public virtual ICollection<Favorite> Favorites { get; set; } // Favorilediği ürünler
    }
}