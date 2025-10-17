using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using PharmacyApp.Shared.models;
using PharmacyApp.Common.Common.Result;


namespace PharmacyApp.Common.Abstractions
{

public interface IEventDispatcher
{
    Task<Result> DispatchAsync(DomainEvent domainEvent);
    Task<Result> DispatchRangeAsync(IEnumerable<DomainEvent> domainEvents);
}

}
