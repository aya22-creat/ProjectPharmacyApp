using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CatalogManagement.CategoryManagement.Events
{
    public class CategoryUpdatedEvent : DomainEvent, INotification
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }
        public string Description { get; }
         public DateTime UpdatedAt { get; }

        public CategoryUpdatedEvent(
            Guid categoryId,
            string categoryName,
            string description,
            DateTime updatedAt)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description;
            UpdatedAt = updatedAt;
        }
    }
}