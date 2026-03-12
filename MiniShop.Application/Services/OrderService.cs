using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPriceStrategy _priceStrategy;
        private readonly IUnitOfWork _uow;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository,
            IUnitOfWork uow, IPriceStrategy priceStrategy)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _uow = uow;
            _priceStrategy = priceStrategy;
        }
        public async Task<Guid> CreateOrderAsync(Guid userId, List<(Guid productId, int quantity)> items)
        {
            var order = new Order(userId);

            foreach (var item in items)
            {
                var product = await _productRepository.GetByIdAsync(item.productId) ??
                               throw new InvalidOperationException("Product not found");

                if (product.Stock < item.quantity)
                    throw new InvalidOperationException("Not enough stock");

                product.DecreasedStock(item.quantity);
                var finalPrice = _priceStrategy.CalculatePrice(product, item.quantity);

                order.AddItem(product.ProductId, finalPrice, item.quantity);

            }

            await _orderRepository.AddAsync(order);
            await _uow.CommitAsync();

            return order.OrderId;
        }

    }
}
