using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Contracts.Product
{
    public class CreateProductRequest
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10000000)]
        public int Quantity { get; set; }
    }
}
