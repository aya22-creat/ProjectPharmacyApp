using MediatR;
using PharmacyApp.Application.Order.DTO;
using OrderAgg = PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;


namespace PharmacyApp.Application.Order.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Items == null || !request.Items.Any())
                throw new ArgumentException("Order must have at least one item.");


            var order = new OrderAgg.Order(
                request.CustomerId,
                request.ShippingAddress ?? string.Empty,
                request.BillingAddress ?? request.ShippingAddress ?? string.Empty,
                request.PaymentMethod ?? "Cash"
            );

            foreach (var item in request.Items)
            {
                var unitPrice = Money.Create(item.UnitPrice);
                order.AddItem(item.ProductId, item.ProductName, item.Quantity, unitPrice);
            }


            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return new OrderDto(order);
        }
    }
}
