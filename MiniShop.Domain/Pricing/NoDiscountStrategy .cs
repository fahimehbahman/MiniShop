using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Domain.Pricing
{
    public class NoDiscountStrategy : IPriceStrategy
    {
        public DiscountType Type => DiscountType.None;
        public NoDiscountStrategy()
        {
            
        }
        public decimal CalculatePrice(Product product, int quantity)
        {
            return product.Price * quantity;
        }
    }
}
