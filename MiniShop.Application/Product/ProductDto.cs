using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Product
{
    public record ProductDto (
        Guid ProductId,
        string Name,
        decimal Price,
        int Quantity
        );
}
