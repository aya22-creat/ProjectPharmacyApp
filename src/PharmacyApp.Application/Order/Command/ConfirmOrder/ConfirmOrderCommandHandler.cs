using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.Order.Command.ConfirmOrder;

public sealed class ConfirmOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ConfirmOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OrderDto> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            throw new Exception("Order not found.");

        order.Confirm();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OrderDto(order);
    }
}
