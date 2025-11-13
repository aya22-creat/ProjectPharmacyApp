using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Application.Common;
using OrderAgg = PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;


namespace PharmacyApp.Application.Order.Command.CreateOrder
{
    public class CreateOrderCommandHandler : BaseCommandHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Items == null || !request.Items.Any())
                throw new InvalidOperationException("Order must have at least one item.");

            var order = new OrderAgg.Order(
                request.CustomerId,
                request.ShippingAddress ?? string.Empty,
                request.BillingAddress ?? request.ShippingAddress ?? string.Empty,
                request.PaymentMethod ?? "Cash"
            );

            foreach (var item in request.Items)
            {
                var unitPrice = Money.Create(item.UnitPrice, Constants.DefaultCurrency);
                order.AddItem(item.ProductId, item.ProductName, item.Quantity, unitPrice);
            }

            await _orderRepository.AddAsync(order, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
