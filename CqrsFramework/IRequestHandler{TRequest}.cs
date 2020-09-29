namespace CqrsFramework
{
    public interface IRequestHandler<TRequest> : IRequestHandler<TRequest, Unit>
        where TRequest : IRequest<Unit>
    {
    }
}