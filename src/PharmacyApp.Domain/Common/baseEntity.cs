using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PharmacyApp.Domain.Common
{
    public abstract class baseEntity<TId>
    {
        public TId? Id { get; protected set; }

        private readonly List<DomainEvent> _domainEvents = new();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected baseEntity() { }
        //Allows the subclass or the ORM to create the object without an Id

        protected baseEntity(TId id)
        {
            Id = id;
        }
        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        public override bool Equals(object? obj)
        {
            if (obj is not baseEntity<TId> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Id == null || other.Id == null)
                return false;

            return Id.Equals(other.Id);
        }
        public static bool operator ==(baseEntity<TId>? left, baseEntity<TId>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(baseEntity<TId>? left, baseEntity<TId>? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }


    
}