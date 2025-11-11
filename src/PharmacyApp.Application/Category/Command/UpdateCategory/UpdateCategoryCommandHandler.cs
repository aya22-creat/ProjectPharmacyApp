
using MediatR;
using PharmacyApp.Application.Category.DTO;

using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Repositories;

namespace PharmacyApp.Application.Category.Command.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)

                throw new InvalidOperationException($"Category with ID {request.Id} not found");


            category.Update(request.Name, request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CategoryDto(category.Id, category.Name, category.ProductCount, category.Description);
        }
    }
}