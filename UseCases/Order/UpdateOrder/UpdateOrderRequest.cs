using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Order;

namespace UseCases.Order.UpdateOrder
{
    public class UpdateOrderRequest 
    {
        public int Id { get; set; }
        public OrderDto Dto { get; set; }
    }
}
