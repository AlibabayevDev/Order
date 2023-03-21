using Order.Core.Domain.Entities;
using Order.WebCore.Mappers;
using Order.WebCore.Models;
using System.Reflection;

namespace Order.WebCore.Mappers
{
    public class OrderMapper : BaseMapper<OrderEntity, OrderModel>
    {
        private readonly ProviderMapper providerMapper=new ProviderMapper();
        public override OrderEntity Map(OrderModel model)
        {
            return new OrderEntity()
            {
                Id = model.Id,
                Number = model.Number,
                Date = model.Date,
                ProviderId = model.ProviderId,  
                Provider=providerMapper.Map(model.Provider)
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
                Provider = providerMapper.Map(entity.Provider)
            };
        }
    }
}