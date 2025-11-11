using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.Command.AddOrderItem
{
    public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Order not found.");

            var unitPrice = Money.Create(request.UnitPrice);
            order.AddItem(request.ProductId, request.ProductName, request.Quantity, unitPrice);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
