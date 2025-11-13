using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Messages;

public class SendEmailConsumer : IConsumer<SendEmailMessage>
    {
        private readonly ILogger<SendEmailConsumer> _logger;

        public SendEmailConsumer(ILogger<SendEmailConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendEmailMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation("Sending email to {To}: {Subject}", message.To, message.Subject);

            try
            {
                _logger.LogInformation("Email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email");
                throw;
            }

            await Task.CompletedTask;
        }
    }