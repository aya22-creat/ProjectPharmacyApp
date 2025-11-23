namespace PharmacyApp.Common.Errors
{
    public interface IError
    {
        string Message { get; }
        string Code { get; }
    }
}
