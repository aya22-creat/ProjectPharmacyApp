using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Category.DTO;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Repositories;
using CategoryAgg = PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate.CategoryAggregate;

namespace PharmacyApp.Application.Category.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category =  await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            
                throw new InvalidOperationException($"Category with ID {request.Id} not found");
            

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}