using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Message.Producer
{
    public record SendPushNotificationMessage(
         Guid UserId,
        string Title,
        string Message,
        string? ActionUrl = null
    );

}

    

