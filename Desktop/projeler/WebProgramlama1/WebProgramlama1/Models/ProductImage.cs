using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramlama1.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; } // Resim yolu: /img/urunler/resim1.jpg

        public bool IsMain { get; set; } = false; // Vitrin resmi mi?

        // İlişki
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}