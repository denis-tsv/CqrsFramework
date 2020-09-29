using CqrsFramework;
using WebApi.Order;
using WebApi.Order.CheckOrder;

namespace UseCases.Order
{
    public class GetOrderRequest : IRequest<OrderDto>, ICheckOrderRequest
    {
        public int Id { get; set; }
    }
}