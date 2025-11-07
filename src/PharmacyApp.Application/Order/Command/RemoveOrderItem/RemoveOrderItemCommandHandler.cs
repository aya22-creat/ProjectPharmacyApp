using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Order.DTO;
using OrderAgg = PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Order not found.");

            order.RemoveItem(request.OrderItemId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
