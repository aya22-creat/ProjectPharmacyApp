


namespace PharmacyApp.Common.Domain
{

    public interface IAggregateRoot // Marker interface
    {
    }
    public abstract class AggregateRoot<TId> : baseEntity<TId>
        where TId : notnull
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public new IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected AggregateRoot(TId id) : base(id) { }

        protected AggregateRoot() : base() { }

        public new void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public new void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}