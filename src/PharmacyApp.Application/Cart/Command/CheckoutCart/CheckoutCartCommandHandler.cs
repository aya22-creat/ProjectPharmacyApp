using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.Command;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Order.Command.CreateOrder;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.CartManagement.Repositories;
using CartEntity = PharmacyApp.Domain.CartManagement.Entities.Cart;

namespace PharmacyApp.Application.Cart.Command.CheckoutCart
{
    public class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, Guid>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CheckoutCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            if (cart.IsEmpty())
                throw new InvalidOperationException("Cannot checkout an empty cart");

            await ValidateProductsAvailability(cart, cancellationToken);

            var orderItems = cart.Items.Select(item => new CreateOrderItemDto(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.Price!.Amount
            )).ToList();

            var createOrderCommand = new CreateOrderCommand(
                request.CustomerId,
                orderItems,
                cart.GetTotal().Currency ?? "EGP",
                request.ShippingAddress,
                request.BillingAddress,
                request.PaymentMethod
            );

            var orderDto = await _mediator.Send(createOrderCommand, cancellationToken);

            cart.MarkAsCheckedOut();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return orderDto.Id;
        }

        private async Task ValidateProductsAvailability(CartEntity cart, CancellationToken cancellationToken)
        {
        
            // foreach (var item in cart.Items)
            // {
            //     var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            //     if (!product.IsAvailable) throw new InvalidOperationException($"{product.Name} is not available");
            // }

            await Task.CompletedTask;
        }
    }
}
