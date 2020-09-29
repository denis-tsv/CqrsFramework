using System;
using System.Collections.Generic;
using System.Text;
using CqrsFramework;
using WebApi.Order;

namespace UseCases.Order.UpdateOrder
{
    public class UpdateOrderRequest : IRequest
    {
        public int Id { get; set; }
        public OrderDto Dto { get; set; }
    }
}
