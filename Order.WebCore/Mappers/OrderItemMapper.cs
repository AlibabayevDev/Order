using Order.Core.Domain.Entities;
using Order.WebCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Mappers
{
    public class OrderItemMapper : BaseMapper<OrderItemEntity, OrderItemModel>
    {
        private readonly OrderMapper orderMapper = new OrderMapper();
        public override OrderItemEntity Map(OrderItemModel model)
        {
            return new OrderItemEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Quantity = model.Quantity,
                Unit=model.Unit,
                Order = orderMapper.Map(model.Order),
                OrderId = model.OrderId,
            };
        }

        public override OrderItemModel Map(OrderItemEntity entity)
        {
            return new OrderItemModel()
            {
                Id=entity.Id,
                Name=entity.Name,
                Order = orderMapper.Map(entity.Order),
                Unit = entity.Unit,
                Quantity=entity.Quantity,
                OrderId=entity.OrderId,
            };
        }
    }
}
