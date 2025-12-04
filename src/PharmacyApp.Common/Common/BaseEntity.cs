using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmacyApp.Common.Common;


namespace PharmacyApp.Common.Common;

    public abstract class BaseEntity<TId>
    {
        public TId? Id { get; protected set; }

        private readonly List<DomainEvent> _domainEvents = new();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected BaseEntity() { }
        //Allows the subclass or the ORM to create the object without an Id

        protected BaseEntity(TId id)
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
            if (obj is not BaseEntity<TId> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Id == null || other.Id == null)
                return false;

            return Id.Equals(other.Id);
        }
        public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }



