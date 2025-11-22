using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Common;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Application.CheckOut.DTO;
using PharmacyApp.Application.Order.Command.CreateOrder;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories;
using CartEntity = PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities.CheckoutAggregate;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;

namespace PharmacyApp.Application.CheckOut.Command.CompleteCheckout
{
    public class CompleteCheckoutCommandHandler : IRequestHandler<CompleteCheckoutCommand, CheckoutResultDto>
    {
        private readonly ICheckOutRepository _checkoutRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMediator _mediator;

        public CompleteCheckoutCommandHandler(
            ICheckOutRepository checkoutRepository,
            ICartRepository cartRepository,
            IMediator mediator)
        {
            _checkoutRepository = checkoutRepository;
            _cartRepository = cartRepository;
            _mediator = mediator;
        }

       public async Task<CheckoutResultDto> Handle(CompleteCheckoutCommand request, CancellationToken cancellationToken)
{
    // Get checkout
    var checkout = await _checkoutRepository.GetByIdAsync(request.CheckoutId, cancellationToken)
        ?? throw new InvalidOperationException($"Checkout with id {request.CheckoutId} not found");

    // Get cart
    var cart = await _cartRepository.GetByIdAsync(checkout.CartId, cancellationToken)
        ?? throw new InvalidOperationException($"Cart with id {checkout.CartId} not found");
    // Map cart items to order items
    var orderItems = cart.Items.Select(item => new CreateOrderItemDto(
        item.ProductId,
        item.ProductName,
        item.Quantity,
        item.Price?.Amount ?? 0m
    )).ToList();

    // Create Order command
    var createOrderCommand = new CreateOrderCommand(
        CustomerId: checkout.CustomerId,
        Items: orderItems,
        Currency: checkout.TotalPrice.Currency,
        ShippingAddress: checkout.ShippingAddress.ToString(),
        BillingAddress: checkout.BillingAddress.ToString(),
        PaymentMethod: checkout.PaymentMethod.Type
    );

    // Send create order command
    var orderDto = await _mediator.Send(createOrderCommand, cancellationToken);

    // Complete checkout
    checkout.CompleteCheckout();
    await _checkoutRepository.UpdateAsync(checkout, cancellationToken);

    // Return result DTO
    return new CheckoutResultDto(
        CheckoutId: checkout.Id,
        OrderId: orderDto.Id,
        Status: CheckoutStatus.Completed.ToString(), 
        Message: "Checkout completed successfully"
    );
}

    }
}   