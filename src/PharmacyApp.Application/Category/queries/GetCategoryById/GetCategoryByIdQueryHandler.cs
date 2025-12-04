using MediatR;
using PharmacyApp.Application.Category.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Domain.CatalogManagement.Category.Repositories;
using PharmacyApp.Domain.CatalogManagement.Category;
using System;


namespace PharmacyApp.Application.Category.queries.GetCatwegoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            return category == null ? null : new CategoryDto(category.Id, category.Name, category.ProductCount, category.Description);
        }
    }
}