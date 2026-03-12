using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; }= null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public OrderItem(Guid orderId, Guid productId, decimal unitPrice, int quantity)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
        public void IncreaseQuantity(int quantity) 
        {
            Quantity += quantity;
        }
    }
}
