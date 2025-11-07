using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Order.DTO;
using OrderAgg = PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
                return null;

            var orderDto = new OrderDto(
                order.Id,
                order.CustomerId,
                order.OrderNumber,
                order.Status.ToString(),
                order.Items.Select(i => new OrderItemDto(
                    i.Id,
                    i.ProductName,
                    i.Quantity,
                    i.UnitPrice.Amount,
                    i.Discount?.Amount ?? 0,
                    0,
                    i.TotalPrice.Amount,
                    string.Empty
                )).ToList(),
                order.SubTotal.Amount,
                order.ShippingCost.Amount,
                order.Tax.Amount,
                order.Discount.Amount,
                order.TotalAmount.Amount,
                "EGP",
                order.ShippingAddress,
                order.BillingAddress,
                order.PaymentMethod,
                order.CreatedAt,
                order.ConfirmedAt,
                order.ShippedAt,
                order.DeliveredAt,
                order.CancelledAt,
                order.CancellationReason ?? string.Empty
            );

            return orderDto;
        }
    }
}
