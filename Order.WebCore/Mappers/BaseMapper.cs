using Order.Core.Domain.Entities;
using Order.WebCore.Models;

namespace Order.WebCore.Mappers
{
    public abstract class BaseMapper<TEntity, TModel> where TEntity : BaseEntity where TModel : BaseModel
    {
        public abstract TEntity Map(TModel model);
        public abstract TModel Map(TEntity entity);
    }
}