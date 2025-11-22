using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer;


namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Category
{
       public class CategoryUpdatedEventPublisher : INotificationHandler<CategoryUpdatedEvent>
    {
        private readonly ILogger<CategoryUpdatedEventPublisher> _logger;

        public CategoryUpdatedEventPublisher(ILogger<CategoryUpdatedEventPublisher> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Category updated: CategoryId={CategoryId}", notification.CategoryId);
            await Task.CompletedTask;
        }
    }
}