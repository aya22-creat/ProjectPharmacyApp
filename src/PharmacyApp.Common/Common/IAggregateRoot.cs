namespace PharmacyApp.Common.Common;

public interface IAggregateRoot { }

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot
    where TId : notnull
{
    protected AggregateRoot() : base() { }
    protected AggregateRoot(TId id) : base(id) { }

    protected void RaiseDomainEvent(DomainEvent domainEvent)
    {
        AddDomainEvent(domainEvent);
    }
}
