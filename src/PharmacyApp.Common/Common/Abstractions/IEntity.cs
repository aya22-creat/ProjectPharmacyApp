using System;
namespace PharmacyApp.Common.Common.models
{
    public abstract class IEntity
    {
        public Guid Id { get; protected set; }
    }
}