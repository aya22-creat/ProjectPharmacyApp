using Microsoft.AspNetCore.Mvc;
using MediatR;
using PharmacyApp.API.Requests.Order;
using PharmacyApp.Application.OrderManagement.Command.CreateOrder;
using PharmacyApp.Application.CartManagement.Command.CheckoutCart;
using PharmacyApp.Application.OrderManagement.Command.CancelOrder;
using PharmacyApp.Application.OrderManagement.Command.ConfirmOrder;
using PharmacyApp.Application.OrderManagement.Command.RejectOrder;
using PharmacyApp.Application.OrderManagement.Command.CompleteOrder;
using PharmacyApp.Application.OrderManagement.queries.GetOrderById;
using PharmacyApp.Application.OrderManagement.Queries.GetCustomerOrders;
using PharmacyApp.Application.OrderManagement.DTO;

namespace PharmacyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }



   [HttpGet("{id:guid}")]
[ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<OrderDto>> GetOrderById(
    Guid id,
    CancellationToken cancellationToken)
{
    var query = new GetOrderByIdQuery(id);
    var order = await _mediator.Send(query, cancellationToken);

    if (order == null)
        return NotFound(new { Message = $"Order with id {id} not found" });

    return Ok(order);
}


   [HttpGet("customer/{customerId:guid}")]
[ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerOrders(
    Guid customerId,
    CancellationToken cancellationToken)
{
    var query = new GetCustomerOrdersQuery(customerId);
    var orders = await _mediator.Send(query, cancellationToken);

    return Ok(orders ?? Enumerable.Empty<OrderDto>());
}

[HttpPost]
[ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderRequest request)
{
    var command = new CreateOrderCommand(
        request.CustomerId,
        request.Items.Select(i => new CreateOrderItemDto(
            i.ProductId,
            i.ProductName,
            i.Quantity,
            i.Price)).ToList(),
        request.Currency,
        request.ShippingAddress,
        request.BillingAddress,
        request.PaymentMethod,
        request.ShippingCost
    );
    var order = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
}


  [HttpPost("{orderId:guid}/cancel")]
[ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<OrderDto>> Cancel(
    Guid orderId,
    [FromBody] CancelOrderRequest request,
    CancellationToken cancellationToken)
{
    var command = new CancelOrderCommand(
        orderId,
        request.CustomerId,
        request.Reason ?? string.Empty
    );

    var order = await _mediator.Send(command, cancellationToken);

    if (order == null)
        return NotFound(new { Message = "Order not found" });

    return Ok(order);
}


    [HttpPost("{orderId:guid}/confirm")]
[ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
public async Task<ActionResult<OrderDto>> Confirm(
    Guid orderId,
    [FromBody] AdminActionRequest request,
    CancellationToken cancellationToken)
{
    var command = new ConfirmOrderCommand(orderId, request.AdminId);
    var order = await _mediator.Send(command, cancellationToken);

    return Ok(order);
}


    [HttpPost("{orderId:guid}/reject")]
[ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
public async Task<ActionResult<OrderDto>> Reject(
    Guid orderId,
    [FromBody] RejectOrderRequest request,
    CancellationToken cancellationToken)
{
    var command = new RejectOrderCommand(
        orderId,
        request.AdminId,
        request.Reason ?? string.Empty
    );

    var order = await _mediator.Send(command, cancellationToken);
    return Ok(order);
}

    [HttpPost("{orderId:guid}/complete")]
[ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
public async Task<ActionResult<OrderDto>> Complete(
    Guid orderId,
    CancellationToken cancellationToken)
{
    var command = new CompleteOrderCommand(orderId);
    var order = await _mediator.Send(command, cancellationToken);

    return Ok(order);
}

    //checkout order
}




