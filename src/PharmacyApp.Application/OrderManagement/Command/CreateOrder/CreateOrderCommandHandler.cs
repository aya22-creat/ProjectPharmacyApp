using PharmacyApp.Application.OrderManagement.DTO;
using PharmacyApp.Application.Common;
using OrderAgg = PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.OrderManagement.Repositories;
using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using PharmacyApp.Domain.OrderManagement.Entities;


namespace PharmacyApp.Application.OrderManagement.Command.CreateOrder
{
    public class CreateOrderCommandHandler : BaseCommandHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public override async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Items == null || !request.Items.Any())
                throw new InvalidOperationException("Order must have at least one item.");

            var order = new OrderAgg.Order(
                request.CustomerId,
                request.ShippingAddress ?? string.Empty,
                request.BillingAddress ?? request.ShippingAddress ?? string.Empty,
                request.PaymentMethod ?? "Cash"
            );

            foreach (var item in request.Items)
            {
                var price = Money.Create(item.Price, Constants.DefaultCurrency);
                var orderItem = new OrderItem(item.ProductId, item.ProductName, item.Quantity, price);
                order.AddItem(orderItem);
            }

            await _orderRepository.AddAsync(order, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            // Update stock for each product in the order
            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product != null)
                {
                    product.UpdateStock(-item.Quantity); // Decrease stock
                    _productRepository.Update(product);
                }
            }
            await SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
    }
}
