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
        private readonly string _connect;

        public SqlOrderRepository(string connect)
        {
            _connect = connect;
        }
        public void Add(OrderEntity bank)
        {
            using (var connection=new SqlConnection(_connect))
            {
                connection.Open();
                string query = "Insert into Order output inserted.id values(@Number,@Date,@ProviderId)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", bank.Id);
                command.Parameters.AddWithValue("Number", bank.Number);
                command.Parameters.AddWithValue("Date", bank.Date);
                command.Parameters.AddWithValue("ProviderId", bank.ProviderId);
                bank.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(OrderEntity bank)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "update Order set Number=@Number,Date=@Date,ProviderId=@ProviderId where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", bank.Id);
                command.Parameters.AddWithValue("Number", bank.Number);
                command.Parameters.AddWithValue("Date", bank.Date);
                command.Parameters.AddWithValue("ProviderId", bank.ProviderId);
                command.ExecuteNonQuery();
            }
        }

        public List<OrderEntity> GetAll()
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Order";
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
                        orders.Add(entity);
                    }

                    return orders;
                }
            }
        }

        public OrderEntity Get(int id)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Order where Id= @id";
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
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "delete from Order where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }

    }
}
