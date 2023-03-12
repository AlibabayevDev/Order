using Order.Core.DataAccess.Sql;
using Order.Core.Domain.Abstract;

namespace Order.Core.Factories
{
    public class DbFactory
    {
        public static IUnitOfWork Create(string connectionString)
        {
            return new SqlUnitOfWork(connectionString);
        }
    }
}
