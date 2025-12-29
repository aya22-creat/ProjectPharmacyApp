using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Consumers;
using PharmacyApp.Infrastructure.MessageQueue.Consumers.Cart;

namespace PharmacyApp.Infrastructure.MessageQueue.Configuration
{
    public static class MassTransitConfiguration
    {
        public static IServiceCollection AddMessageQueue(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSettings = new RabbitMqSettings();
            configuration.GetSection("RabbitMqSettings").Bind(rabbitMqSettings);

            services.AddMassTransit(x =>
            {
                // Register consumers
                x.AddConsumer<OrderCompletedConsumer>();
                x.AddConsumer<OrderCreatedConsumer>();
                x.AddConsumer<CartAbandonedConsumer>();
                x.AddConsumer<ItemAddedToCartConsumer>();
                x.AddConsumer<SendEmailConsumer>();
                x.AddConsumer<SendPushNotificationConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    // Host configuration
                    cfg.Host($"rabbitmq://{rabbitMqSettings.Host}:{rabbitMqSettings.Port}{rabbitMqSettings.VirtualHost}", h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                    });

                    // Receive endpoints for consumers
                    cfg.ReceiveEndpoint("order-created-queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    });

                    cfg.ReceiveEndpoint("order-completed-queue", e =>
                    {
                        e.ConfigureConsumer<OrderCompletedConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    });

                    cfg.ReceiveEndpoint("cart-abandoned-queue", e =>
                    {
                        e.ConfigureConsumer<CartAbandonedConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    });

                    cfg.ReceiveEndpoint("item-added-to-cart-queue", e =>
                    {
                        e.ConfigureConsumer<ItemAddedToCartConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    });

                    cfg.ReceiveEndpoint("send-email-queue", e =>
                    {
                        e.ConfigureConsumer<SendEmailConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    });

                    cfg.ReceiveEndpoint("send-push-notification-queue", e =>
                    {
                        e.ConfigureConsumer<SendPushNotificationConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

                        // Optional policies
                        cfg.UseCircuitBreaker(cb =>
                        {
                            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                            cb.TripThreshold = 15;
                            cb.ActiveThreshold = 10;
                            cb.ResetInterval = TimeSpan.FromMinutes(5);
                        });

                        cfg.UseRateLimit(1000, TimeSpan.FromSeconds(1));
                    });
                });
            });

            return services;
        }
    }
}
