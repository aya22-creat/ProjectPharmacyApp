using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CatalogManagement.CategoryManagement.Events{
 public class CategoryDeletedEvent : DomainEvent, INotification
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }
        public DateTime DeletedAt { get; }

        public CategoryDeletedEvent(Guid categoryId)
        {
            CategoryId = categoryId;
            CategoryName = string.Empty;
            DeletedAt = DateTime.UtcNow;

        }
    }
}