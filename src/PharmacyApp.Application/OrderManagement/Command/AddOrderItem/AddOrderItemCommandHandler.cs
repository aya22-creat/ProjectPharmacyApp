using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;
using PharmacyApp.Application.Common;
using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.OrderManagement.Repositories;
using PharmacyApp.Domain.OrderManagement.Entities;

namespace PharmacyApp.Application.OrderManagement.Command.AddOrderItem
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

            var price = Money.Create(request.Price.Amount, Constants.DefaultCurrency);
            var orderItem = new OrderItem(request.ProductId, request.ProductName, request.Quantity, price);
            order.AddItem(orderItem);

            await SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
