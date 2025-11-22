using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Category
{
    public record CategoryCreatedMessage(
        Guid CategoryId,
        string CategoryName,
        Guid? ParentCategoryId,
        string? Description,
        int DisplayOrder,
        DateTime CreatedAt
    );
}
