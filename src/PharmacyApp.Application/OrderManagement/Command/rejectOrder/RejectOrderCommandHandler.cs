using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.OrderManagement.Command.RejectOrder;

public sealed class RejectOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RejectOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OrderDto> Handle(RejectOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            throw new Exception("Order not found.");

        order.Reject(request.Reason);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OrderDto(order);
    }
}
