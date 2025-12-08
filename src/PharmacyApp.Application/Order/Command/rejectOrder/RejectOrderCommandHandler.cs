using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.Command.RejectOrder{
    public class RejectOrderCommandHandler : IRequestHandler<RejectOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Handle(RejectOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Order not found.");

            order.Cancel(request.Reason);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
