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
    public class ProviderMapper : BaseMapper<ProviderEntity, ProviderModel>
    {
        public override ProviderEntity Map(ProviderModel model)
        {
            if(model==null)
            {
                return new ProviderEntity();
            }
            return new ProviderEntity()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public override ProviderModel Map(ProviderEntity entity)
        {
            if (entity == null)
            {
                return new ProviderModel();
            }
            return new ProviderModel()
            {
                Id=entity.Id,
                Name=entity.Name,
            };
        }
    }
}
