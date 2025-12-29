using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.API.Requests.Category;
using PharmacyApp.Application.CatalogManagement.Category.Command.CreateCategory;
using PharmacyApp.Application.CatalogManagement.Category.Command.UpdateCategory;
using PharmacyApp.Application.CatalogManagement.Category.Command.DeleteCategory;
using PharmacyApp.Application.CatalogManagement.Category.queries.GetCategories;
using PharmacyApp.Application.CatalogManagement.Category.queries.GetCategoryById;
using PharmacyApp.Application.CatalogManagement.Category.DTO;


namespace PharmacyApp.API.Controllers;



[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories(CancellationToken cancellationToken)
    {
        var query = new GetCategoriesQuery();
        var categories = await _mediator.Send(query, cancellationToken);
        return Ok(categories);
    
    }
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id, CancellationToken cancellationToken)
        {
        
            var query = new GetCategoryByIdQuery(id);
            var category = await _mediator.Send(query, cancellationToken);

            if (category == null)
                return NotFound(new { Message = $"Category with ID {id} not found" });

            return Ok(category);
        }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var command = new CreateCategoryCommand(request.Name, request.Description);
            var category = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.Id },
                category);
        }

     [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = new UpdateCategoryCommand(id, request.Name, request.Description);
                var category = await _mediator.Send(command, cancellationToken);
                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(Guid id , CancellationToken cancellationToken)
        {
            try
            {   var command = new DeleteCategoryCommand(id);
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }