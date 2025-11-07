using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;
using PharmacyApp.Application.Order.Queries.GetCustomerOrders;

namespace PharmacyApp.Application.Order.Queries.GetCustomerOrders
{
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetCustomerOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (orders == null || !orders.Any())
                return new List<OrderDto>();

            var result = orders.Select(order => new OrderDto(
                Id: order.Id,
                CustomerId: order.CustomerId,
                OrderNumber: order.OrderNumber,
                Status: order.Status.ToString(),

                Items: order.Items.Select(i => new OrderItemDto(
                    Id: i.Id,
                    Name: i.ProductName,
                    Quantity: i.Quantity,
                    Price: i.UnitPrice.Amount,
                    Discount: i.Discount?.Amount ?? 0,
                    Tax: 0,
                    Total: i.TotalPrice.Amount,
                    Note: string.Empty
                )).ToList(),

                SubTotal: order.SubTotal.Amount,
                ShippingCost: order.ShippingCost.Amount,
                Tax: order.Tax.Amount,
                Discount: order.Discount.Amount,
                TotalAmount: order.TotalAmount.Amount,

                Currency: "EGP",
                ShippingAddress: order.ShippingAddress,
                BillingAddress: order.BillingAddress,
                PaymentMethod: order.PaymentMethod,

                CreatedAt: order.CreatedAt,
                ConfirmedAt: order.ConfirmedAt,
                ShippedAt: order.ShippedAt,
                DeliveredAt: order.DeliveredAt,
                CancelledAt: order.CancelledAt,
                CancellationReason: order.CancellationReason ?? string.Empty
            )).ToList();

            return result;
        }
    }
}
