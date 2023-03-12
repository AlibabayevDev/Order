using Order.Core.Domain.Entities;
using Order.WebCore.Mappers;
using Order.WebCore.Models;

namespace Order.WebCore.Mappers
{
    public class OrderMapper : BaseMapper<OrderEntity, OrderModel>
    {
        public override OrderEntity Map(OrderModel model)
        {
            return new OrderEntity()
            {
                Id = model.Id,
                Number = model.Number,
                Date = model.Date,
                ProviderId = model.ProviderId,  
            };
        }

        public override OrderModel Map(OrderEntity entity)
        {
            return new OrderModel()
            {
                Id = entity.Id,
                Number=entity.Number,
                Date=entity.Date,
                ProviderId=entity.ProviderId,
            };
        }
    }
}