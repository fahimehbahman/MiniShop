using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Domain.Pricing
{
    public interface IPriceStrategy
    {
        DiscountType Type { get; }
        decimal CalculatePrice(Product product, int quantity);
    }
}
