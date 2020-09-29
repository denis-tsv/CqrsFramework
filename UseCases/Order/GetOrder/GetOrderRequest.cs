using CqrsFramework;
using WebApi.Order;

namespace UseCases.Order
{
    public class GetOrderRequest : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}