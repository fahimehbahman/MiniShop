using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.Contracts.Orders;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using System.Security.Claims;

namespace MiniShop.Api
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(
            IOrderService orderService,
            ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var orderId = await _orderService.CreateOrderAsync(userId,
                request.Items
                       .Select(x => (x.ProductId, x.Quantity))
                       .ToList());
            _logger.LogInformation(
            "User {UserId} created order {OrderId}",
            userId,
            orderId );

            return Ok(new CreateOrderResponse
            {
                OrderId = orderId
            });

        }
    }
}
