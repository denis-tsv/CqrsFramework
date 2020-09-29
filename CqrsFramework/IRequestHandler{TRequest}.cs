namespace CqrsFramework
{
    public interface IRequestHandler<TRequest> : IRequestHandler<TRequest, Unit>
    {
    }
}