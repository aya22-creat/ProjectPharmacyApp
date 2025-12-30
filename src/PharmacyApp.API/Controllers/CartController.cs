using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.API.Requests.Cart;
using PharmacyApp.Application.CartManagement.Command.AddItem;
using PharmacyApp.Application.CartManagement.Command.RemoveItem;
using PharmacyApp.Application.CartManagement.Command.UpdateCart;
using PharmacyApp.Application.CartManagement.Command.DeleteCart;
using PharmacyApp.Application.CartManagement.Command.CheckoutCart;
using PharmacyApp.Application.CartManagement.Queries.GetCartByCustomer;
using PharmacyApp.Application.CartManagement.Queries.GetCartTotal;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.API.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

   

    
    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartDto>> GetCart(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var query = new GetCartByCustomerQuery(customerId);
        var cart = await _mediator.Send(query, cancellationToken);

        if (cart == null)
            return NotFound(new { Message = "Cart not found" });

        return Ok(cart);
    }

   
    [HttpGet("{customerId:guid}/total")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTotal(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var query = new GetCartTotalQuery(customerId);
        var total = await _mediator.Send(query, cancellationToken);

        return Ok(new { Total = total });
    }

    [HttpPost("{customerId:guid}/items/add")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CartDto>> AddItem(
        Guid customerId,
        [FromBody] List<AddItemRequest> requests,
        CancellationToken cancellationToken)
    {
        var items = requests.Select(r =>
            new AddToCartItemDto(
                r.ProductId,
                r.ProductName,
                r.Price,
                r.Quantity,
                r.Currency
            )
        ).ToList();

        var command = new AddItemsToCartCommand(customerId, items);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{customerId:guid}/items/{cartItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateItemQuantity(
        Guid customerId,
        Guid cartItemId,
        [FromBody] UpdateQuantityRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCartItemQuantityCommand(
            customerId,
            cartItemId,
            request.NewQuantity
        );

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
            return NotFound(new { Message = "Cart item not found" });

        return Ok(result);
    }
    

    
    [HttpDelete("{customerId:guid}/items/{cartItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveItem(
        Guid customerId,
        Guid cartItemId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveItemFromCartCommand(customerId, cartItemId);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ClearCart(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCartCommand(customerId);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPost("{customerId:guid}/checkout")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> Checkout(
        Guid customerId,
        [FromBody] CheckoutCartRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CheckoutCartCommand(
            customerId,
            request.ShippingAddress,
            request.BillingAddress,
            request.PaymentMethod
        );

        var orderId = await _mediator.Send(command, cancellationToken);

        return Ok(new { OrderId = orderId });
    }
}
