using System.ComponentModel.DataAnnotations;

namespace WebProgramlama1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        // İlişki: Bir kategoride birden çok ürün olabilir.
        public virtual ICollection<Product> Products { get; set; }
    }
}