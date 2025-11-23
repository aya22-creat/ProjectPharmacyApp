using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CartManagement.Events.Coupon;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Cart
{
     public class CouponAppliedToCartEventPublisher : INotificationHandler<CouponAppliedToCartEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CouponAppliedToCartEventPublisher> _logger;

        public CouponAppliedToCartEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CouponAppliedToCartEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CouponAppliedToCartEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Coupon applied to cart: CartId={CartId}, CouponCode={CouponCode}",
                notification.CartId,
                notification.CouponCode
            );

            await Task.CompletedTask;
        }
    }
}