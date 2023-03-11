using PaymentOrder.WebCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentOrder.WebCore.Services.Contracts
{
   public interface IService<TModel> where TModel : BaseModel
    {
        List<TModel> GetAll();
        TModel Get(int id);
        void Delete(int id);
        void Save(TModel model);
    }
}
