using System;
using System.Collections.Generic;
using System.Text;
using CqrsFramework;
using UseCases.Order.Utils;
using WebApi.Order;

namespace UseCases.Order.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDto>, ICheckOrderRequest
    {
        public int Id { get; set; }
    }
}
