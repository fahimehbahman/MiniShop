using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Domain.Pricing
{
    public class FixedAmountDiscountStrategy : IPriceStrategy
    {
        public DiscountType Type => DiscountType.FixedAmount;

        public decimal CalculatePrice(Product product, int quantity)
        {
            var basePrice = product.Price * quantity;

            return basePrice - product.DiscountValue;
        }
    }
}
