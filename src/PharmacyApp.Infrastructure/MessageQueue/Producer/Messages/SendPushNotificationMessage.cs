using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Messages
{
    public record SendPushNotificationMessage(
         Guid UserId,
        string Title,
        string Message,
        string? ActionUrl = null
    );

}

    

