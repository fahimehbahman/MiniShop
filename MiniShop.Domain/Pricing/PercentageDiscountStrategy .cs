using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Domain.Pricing
{
    public class PercentageDiscountStrategy : IPriceStrategy
    {
        public DiscountType Type => DiscountType.Percentage;

        public decimal CalculatePrice(Product product, int quantity)
        {
            var basePrice = product.Price * quantity;

            return basePrice - (basePrice * product.DiscountValue);
        }
    }
}
