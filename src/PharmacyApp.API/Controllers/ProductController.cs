using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.API.Requests.Product;
using PharmacyApp.Application.CatalogManagement.Product.Command.CreateProduct;
using PharmacyApp.Application.CatalogManagement.Product.Command.UpdateProduct;
using PharmacyApp.Application.CatalogManagement.Product.Command.DeleteProduct;
using PharmacyApp.Application.CatalogManagement.Product.Queries.GetProducts;
using PharmacyApp.Application.CatalogManagement.Product.Queries.GetProductById;
using PharmacyApp.Application.CatalogManagement.Product.DTO;
namespace PharmacyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(CancellationToken cancellationToken)
    {
        var query = new GetProductQuery();
        var product = await _mediator.Send(query, cancellationToken);
        return Ok(product);
    }
   [HttpGet("{id:guid}")]

    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
   public async Task<ActionResult<ProductDto>> GetProductById(Guid id , CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var product = await _mediator.Send(query, cancellationToken);

         if (product == null)
            return NotFound(new { Message = $"Product with ID {id} not found" });
        return Ok(product);
    }
   
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductRequest request , CancellationToken cancellationToken)
{
    var command = new CreateProductCommand(
        request.ProductName,
        request.Description,
        request.Price,
        request.Currency ?? "EGP",
        request.CategoryId,
        request.StockQuantity,
        request.IsCosmetic,
        request.IsAvailable
    );

    var product = await _mediator.Send(command, cancellationToken);

    return CreatedAtAction(
        nameof(GetProductById),
        new { id = product.Id },
        product);
}

[HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
        {
            var command = new UpdateProductCommand(
                id,
                request.ProductName,
                request.Description,
                request.Price,
                request.Currency ?? "EGP",
                request.CategoryId,
                request.StockQuantity
            );

            var product = await _mediator.Send(command);

            return Ok(product);
        }

         [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand(id));
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }


}
}