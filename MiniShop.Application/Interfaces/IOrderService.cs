using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid userId,List<(Guid productId, int quantity)> items);
    }
}
