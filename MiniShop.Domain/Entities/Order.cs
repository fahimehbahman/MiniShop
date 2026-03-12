
namespace MiniShop.Domain.Entities;
public class Order
{
    public Guid OrderId { get; private set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Product.Price);

    public Order(Guid userId)
    {
        OrderId = Guid.NewGuid();
        UserId = userId;
        CreatedAt = DateTime.Now;

    }

    public void AddItem(Guid productId, decimal unitPrice, int quantity)
    {
        var existingItem = Items
        .FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
            return;
        }
        Items.Add(new OrderItem
        (
            OrderId ,
            productId ,
            unitPrice,
            quantity
        ));
    }

}


