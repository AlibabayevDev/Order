using Order.Core.Domain.Entities;
using Order.WebCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Mappers
{
    public class OrderItemMapper : BaseMapper<OrderItemEntity, OrderItemModel>
    {
        public override OrderItemEntity Map(OrderItemModel model)
        {
            return new OrderItemEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Quantity = model.Quantity,
                Unit=model.Unit,
                OrderId = model.OrderId,
            };
        }

        public override OrderItemModel Map(OrderItemEntity entity)
        {
            return new OrderItemModel()
            {
                Id=entity.Id,
                Name=entity.Name,
                OrderId=entity.OrderId,
                Unit=entity.Unit,
                Quantity=entity.Quantity,
            };
        }
    }
}
