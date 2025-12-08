using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.Command.CancelOrder;

public sealed class CancelOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CancelOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OrderDto> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            throw new Exception("Order not found.");

        if (order.CustomerId != request.CustomerId)
            throw new UnauthorizedAccessException("You are not the owner of this order.");

        order.Cancel(request.Reason);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OrderDto(order);
    }
}

