using System.Threading.Tasks;

namespace CqrsFramework
{
    public delegate Task<TResponse> HandlerDelegate<TResponse>();
}