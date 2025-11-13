using System;
namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Messages
{

public record SendEmailMessage(
        string To,
        string Subject,
        string Body,
        string? TemplateId = null,
        object? TemplateData = null
    );
}