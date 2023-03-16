using Order.Core.Domain.Abstract;
using Order.Core.Domain.Entities;
using System.Data.SqlClient;

namespace Order.Core.DataAccess.Sql
{
    public class SqlOrderItemRepository : IOrderItemRepository
    {
        private readonly string connectionString;

        public SqlOrderItemRepository(string connect)
        {
            connectionString = connect;
        }

        public void Add(OrderItemEntity orderItem)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Insert into OrderItem output inserted.id values(@Name,@Quantity,@Unit,@OrderId)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", orderItem.Id);
                command.Parameters.AddWithValue("Name", orderItem.Name);
                command.Parameters.AddWithValue("Quantity", orderItem.Quantity);
                command.Parameters.AddWithValue("Unit", orderItem.Unit);
                command.Parameters.AddWithValue("OrderId", orderItem.Order.Id);

                orderItem.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(OrderItemEntity orderItem)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "update Order set Name=@Name,Quantity=@Quantity,Unit=@Unit,OrderId=@OrderId where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", orderItem.Id);
                command.Parameters.AddWithValue("Name", orderItem.Name);
                command.Parameters.AddWithValue("Quantity", orderItem.Quantity);
                command.Parameters.AddWithValue("OrderId", orderItem.Order.Id);
                command.Parameters.AddWithValue("Unit", orderItem.Unit);
                command.ExecuteNonQuery();
            }
        }

        public List<OrderItemEntity> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select OrderItem.Id,orderItem.Name,orderItem.OrderId,orderItem.Quantity,orderItem.Unit,Orders.Id as OrderId,Orders.number,Orders.ProviderId,Orders.Date from OrderItem inner join Orders on OrderItem.OrderId = Orders.Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<OrderItemEntity> orderItems = new List<OrderItemEntity>();
                    while (reader.Read())
                    {
                        OrderItemEntity entity = new OrderItemEntity();
                        entity.Id = Convert.ToInt32(reader["Id"]);
                        entity.Name = Convert.ToString(reader["Name"]);
                        entity.Quantity = Convert.ToInt32(reader["Quantity"]);
                        entity.Unit = Convert.ToString(reader["Unit"]);
                        entity.Order = new OrderEntity()
                        {
                            Id = Convert.ToInt32(reader["OrderId"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            Number = Convert.ToString(reader["Number"]),
                            ProviderId = Convert.ToInt32(reader["ProviderId"])
                        };

                        orderItems.Add(entity);
                    }

                    return orderItems;
                }
            }
        }

        public OrderItemEntity Get(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select OrderItem.Id,orderItem.Name,orderItem.OrderId,orderItem.Quantity,orderItem.Unit,Orders.Id,Orders.number,Orders.ProviderId,Orders.Date from OrderItem inner join Orders on OrderItem.OrderId = Orders.Id where OrderItem.Id= @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    OrderItemEntity entity = new OrderItemEntity();
                    while (reader.Read())
                    {
                        entity.Id = Convert.ToInt32(reader["Id"]);
                        entity.Name = Convert.ToString(reader["Name"]);
                        entity.Quantity = Convert.ToInt32(reader["Quantity"]);
                        entity.Unit = Convert.ToString(reader["Unit"]);
                        entity.Order = new OrderEntity()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            Number = Convert.ToString(reader["Number"]),
                            ProviderId = Convert.ToInt32(reader["ProviderId"])
                        };

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
                string query = "delete from OrderItem where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
