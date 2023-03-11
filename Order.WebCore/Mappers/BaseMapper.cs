using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.WebCore.Models;

namespace PaymentOrder.WebCore.Mappers
{
    public abstract class BaseMapper<TEntity, TModel> where TEntity : BaseEntity where TModel : BaseModel
    {
        public abstract TEntity Map(TModel model);
        public abstract TModel Map(TEntity entity);
    }
}