using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Coupon;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Cart
{
    public class CouponRemovedFromCartEventPublisher : INotificationHandler<CouponRemovedFromCartEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CouponRemovedFromCartEventPublisher> _logger;

        public CouponRemovedFromCartEventPublisher(
            IPublishEndpoint publishEndpoint,
        ILogger<CouponRemovedFromCartEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CouponRemovedFromCartEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Coupon removed from cart: CartId={CartId}", notification.CartId);
            await Task.CompletedTask;
        }
    }
}