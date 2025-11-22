using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Events;

using PharmacyApp.Infrastructure.MessageQueue.Producer;


namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Category
{
     public class CategoryDeletedEventPublisher : INotificationHandler<CategoryDeletedEvent>
    {
        private readonly ILogger<CategoryDeletedEventPublisher> _logger;

        public CategoryDeletedEventPublisher(ILogger<CategoryDeletedEventPublisher> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Category deleted: CategoryId={CategoryId}", notification.CategoryId);
            await Task.CompletedTask;
        }
    }
}