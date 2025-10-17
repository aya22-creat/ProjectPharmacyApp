namespace Shared.Models
{
    public abstract class DomainRule
    {
        public abstract bool IsBroken();
        public abstract string Message { get; }
    }
}
