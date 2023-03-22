using Order.Core.Domain.Abstract;
using Order.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.DataAccess.Sql
{
    public class SqlOrderRepository:IOrderRepository
    {
        private readonly string connectionString;

        public SqlOrderRepository(string connect)
        {
            connectionString = connect;
        }
        public void Add(OrderEntity order)
        {
            using (var connection=new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Insert into Orders output inserted.id values(@Number,@Date,@ProviderId)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", order.Id);
                command.Parameters.AddWithValue("Number", order.Number);
                command.Parameters.AddWithValue("Date", DateTime.Now);
                command.Parameters.AddWithValue("ProviderId", order.ProviderId);
                order.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(OrderEntity order)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "update Orders set Number=@Number,Date=@Date,ProviderId=@ProviderId where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", order.Id);
                command.Parameters.AddWithValue("Number", order.Number);
                command.Parameters.AddWithValue("Date", order.Date);
                command.Parameters.AddWithValue("ProviderId", order.ProviderId);
                command.ExecuteNonQuery();
            }
        }

        public List<OrderEntity> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select Orders.Id,Orders.Date,Orders.Number,Orders.ProviderId,Provider.Id as ProvideId,Provider.Name from Orders inner join Provider on Orders.ProviderId = Provider.Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<OrderEntity> orders = new List<OrderEntity>();
                    while (reader.Read())
                    {
                        OrderEntity entity = new OrderEntity();
                        entity.Id = Convert.ToInt32(reader["Id"]);
                        entity.Number = Convert.ToString(reader["Number"]);
                        entity.Date = Convert.ToDateTime(reader["Date"]);
                        entity.ProviderId = Convert.ToInt32(reader["ProviderId"]);
                        entity.Provider = new ProviderEntity()
                        {
                            Id = Convert.ToInt32(reader["ProvideId"]),
                            Name = Convert.ToString(reader["Name"])
                        };
                        orders.Add(entity);
                    }

                    return orders;
                }
            }
        }

        public OrderEntity Get(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Orders where Id= @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    OrderEntity entity = new OrderEntity();
                    while (reader.Read())
                    {
                        entity.Id = Convert.ToInt32(reader["Id"]);
                        entity.Number = Convert.ToString(reader["Number"]);
                        entity.Date = Convert.ToDateTime(reader["Date"]);
                        entity.ProviderId = Convert.ToInt32(reader["ProviderId"]);

                    }
                    return entity;
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "delete from Orders where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }

        public OrderEntity CheckOrder(int ProviderId, string OrderNumber)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Orders where ProviderId = @ProviderId AND Number=@OrderNumber";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("ProviderId", ProviderId);
                    command.Parameters.AddWithValue("OrderNumber", OrderNumber);

                    SqlDataReader reader = command.ExecuteReader();
                    OrderEntity entity = new OrderEntity();
                    while (reader.Read())
                    {
                        entity.Id = Convert.ToInt32(reader["Id"]);
                        entity.Number = Convert.ToString(reader["Number"]);
                        entity.Date = Convert.ToDateTime(reader["Date"]);
                        entity.ProviderId = Convert.ToInt32(reader["ProviderId"]);

                    }
                    return entity;
                }
            }
        }

        public OrderEntity CheckOrderId(int OrderId,int ProviderId, string OrderNumber)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Orders where ProviderId = @ProviderId AND Number=@OrderNumber AND @Id=Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("ProviderId", ProviderId);
                    command.Parameters.AddWithValue("OrderNumber", OrderNumber);
                    command.Parameters.AddWithValue("Id", OrderId);

                    SqlDataReader reader = command.ExecuteReader();
                    OrderEntity entity = new OrderEntity();
                    while (reader.Read())
                    {
                        entity.Id = Convert.ToInt32(reader["Id"]);
                        entity.Number = Convert.ToString(reader["Number"]);
                        entity.Date = Convert.ToDateTime(reader["Date"]);
                        entity.ProviderId = Convert.ToInt32(reader["ProviderId"]);

                    }
                    return entity;
                }
            }
        }
    }
}
