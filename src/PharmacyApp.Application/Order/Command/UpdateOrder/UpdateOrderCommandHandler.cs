using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Order not found.");

            order.Update(request.Reason);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
