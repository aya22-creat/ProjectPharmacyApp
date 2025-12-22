using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;
using PharmacyApp.Domain.OrderManagement.Repositories;
using PharmacyApp.Application.OrderManagement.Queries.GetCustomerOrders;

namespace PharmacyApp.Application.OrderManagement.Queries.GetCustomerOrders
{
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetCustomerOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (orders == null || !orders.Any())
                return new List<OrderDto>();
            var result = orders.Select(static order => new OrderDto(order)).ToList();

            return result;
          
        }
    }
}
