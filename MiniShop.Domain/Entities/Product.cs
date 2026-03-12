using MiniShop.Domain.Enums;
using System;

namespace MiniShop.Domain.Entities;

public class Product
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public decimal DiscountValue { get; private set; }

    public Product(string name, decimal price, int stock)
    {
        ProductId = Guid.NewGuid();
        Name = name;
        Price = price;
        Stock = stock;
        DiscountType = DiscountType.None;
        DiscountValue = 0;
    }
    public Product(string name, decimal price, int stock, DiscountType discountType,
        decimal discountValue)
    {
        ProductId = Guid.NewGuid();
        Name = name;
        Price = price;
        Stock = stock;
        DiscountType = discountType;
        DiscountValue = discountValue;
    }

    public void DecreasedStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException("Not enough stock");

        Stock -= quantity;
    }
}
