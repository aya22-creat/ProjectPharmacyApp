 using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Message.Producer;

 
 
 public class SendPushNotificationConsumer : IConsumer<SendPushNotificationMessage>
    {
        private readonly ILogger<SendPushNotificationConsumer> _logger;

        public SendPushNotificationConsumer(ILogger<SendPushNotificationConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendPushNotificationMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation(
                "Sending push notification to UserId={UserId}: {Title}",
                message.UserId,
                message.Title
            );

          

            await Task.CompletedTask;
        }
    }
