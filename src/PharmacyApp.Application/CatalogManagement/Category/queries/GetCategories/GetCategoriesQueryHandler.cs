using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.CatalogManagement.Category.DTO;
using PharmacyApp.Domain.CatalogManagement.Category.Repositories;


namespace PharmacyApp.Application.CatalogManagement.Category.queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);

            return categories.Select(static c => new CategoryDto(c.Id, c.Name, c.ProductCount, c.Description, c.DisplayOrder));
        }
    }
}