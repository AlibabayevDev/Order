using Order.Core.Domain.Entities;
using Order.WebCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Mappers
{
    public class ProviderId : BaseMapper<ProviderEntity, ProviderModel>
    {
        public override ProviderEntity Map(ProviderModel model)
        {
            return new ProviderEntity()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public override ProviderModel Map(ProviderEntity entity)
        {
            return new ProviderModel()
            {
                Id=entity.Id,
                Name=entity.Name,
            };
        }
    }
}
