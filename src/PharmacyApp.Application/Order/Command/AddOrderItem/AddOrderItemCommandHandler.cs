using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.Command.AddOrderItem
{
    public class AddOrderItemCommandHandler : BaseCommandHandler<AddOrderItemCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<OrderDto> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new InvalidOperationException("Order not found.");

            var unitPrice = Money.Create(request.UnitPrice, Constants.DefaultCurrency);
            order.AddItem(request.ProductId, request.ProductName, request.Quantity, unitPrice);

            await SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
