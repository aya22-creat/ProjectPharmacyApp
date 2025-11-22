using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common.DomainEvent;
using MediatR;


namespace PharmacyApp.Domain.CatalogManagement.CategoryManagement.Events{
 public class CategoryCreatedEvent : DomainEvent, INotification
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }
        public Guid? ParentCategoryId { get; }
        public string? Description { get; }
        public int DisplayOrder { get; }
        public DateTime CreatedAt { get; }

        public CategoryCreatedEvent(
            Guid categoryId,
            string categoryName,
            Guid? parentCategoryId,
            string? description,
            int displayOrder,
            DateTime createdAt)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            ParentCategoryId = parentCategoryId;
            Description = description;
            DisplayOrder = displayOrder;
            CreatedAt = createdAt;
        }
    }
}