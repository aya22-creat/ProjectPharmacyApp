using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Category;
using PharmacyApp.Infrastructure.MessageQueue.Producer;


namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Category
{
     public class CategoryCreatedEventPublisher : INotificationHandler<CategoryCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CategoryCreatedEventPublisher> _logger;

        public CategoryCreatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CategoryCreatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Publishing CategoryCreatedMessage to queue: CategoryId={CategoryId}",
                notification.CategoryId
            );

            var message = new CategoryCreatedMessage(
                notification.CategoryId,
                notification.CategoryName,
                notification.ParentCategoryId,
                notification.Description,
                notification.DisplayOrder,
                notification.CreatedAt
            );

            await _publishEndpoint.Publish(message, cancellationToken);

            _logger.LogInformation("CategoryCreatedMessage published successfully");
        }
    }
}
