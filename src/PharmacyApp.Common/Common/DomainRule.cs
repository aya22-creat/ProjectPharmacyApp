namespace PharmacyApp.Common.Common
{
    public abstract class DomainRule
    {
        public abstract bool IsBroken();
        public abstract string Message { get; }
    }
}
