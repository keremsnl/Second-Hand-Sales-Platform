using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramlama1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün başlığı zorunludur.")]
        [StringLength(200)]
        [Display(Name = "Ürün Başlığı")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Column(TypeName = "decimal(18,2)")] // Hassas para verisi için
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Stok Adedi")]
        public int Stock { get; set; } = 1; // İkinci el olduğu için varsayılan 1

        public bool IsSold { get; set; } = false; // Satıldı mı?

        [Display(Name = "Yayınlanma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // --- Foreign Keys ---

        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Required]
        public string SellerId { get; set; } // Satıcı (User) ID'si
        
        [ForeignKey("SellerId")]
        public virtual ApplicationUser Seller { get; set; }

        // --- Navigation Properties ---
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Favorite> FavoritedByUsers { get; set; }
    }
}

