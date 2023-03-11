using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Abstract
{
    public interface ICrudRepository<T>
    {
        void Add(T value);
        void Update(T value);
        List<T> GetAll();
        T Get(int id);
        void Delete(int id);
    }
}
