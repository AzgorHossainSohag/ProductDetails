using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductDetails.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public DateOnly AvailableFrom { get; set; }

        [ForeignKey(nameof(Category))]
        [JsonIgnore]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

   
    }
}
