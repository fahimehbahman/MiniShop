using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Contracts.Orders
{
    public class CreateOrderItemDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1,1000)]
        public int Quantity { get; set; }
    }
}
