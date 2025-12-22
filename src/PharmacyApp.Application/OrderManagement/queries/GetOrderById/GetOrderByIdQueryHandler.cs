using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;
using OrderAgg = PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.OrderManagement.queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
                return null;

            return new OrderDto(order);
        }
    }
}
