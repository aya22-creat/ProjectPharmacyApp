using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.CatalogManagement.Category.DTO;
using PharmacyApp.Domain.CatalogManagement.Category.Repositories;
using CategoryAgg = PharmacyApp.Domain.CatalogManagement.Category.CategoryAggregate.CategoryAggregate;


namespace PharmacyApp.Application.CatalogManagement.Category.Command.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = CategoryAgg.Create(request.Name, request.Description);

            await _categoryRepository.AddAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CategoryDto(category.Id, category.Name, category.ProductCount, category.Description);
        }
    }
}