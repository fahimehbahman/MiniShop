using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Contracts.Orders
{
    public class CreateOrderRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "Order must have at least one item")]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
