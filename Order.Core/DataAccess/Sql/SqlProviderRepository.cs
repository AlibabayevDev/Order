using Order.Core.Domain.Abstract;
using Order.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.DataAccess.Sql
{
    public class SqlProviderRepository : IProviderRepository
    {
        private readonly string connectionString;

        public SqlProviderRepository(string connect)
        {
            connectionString = connect;
        }

        public void Add(ProviderEntity provider)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Insert into Provider output inserted.id values(@Name)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Name", provider.Name);
                provider.Id = Convert.ToInt32(command.ExecuteScalar());

            }
        }

        public void Update(ProviderEntity provider)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "update Provider set Name=@Name where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", provider.Id);
                command.Parameters.AddWithValue("Name", provider.Name);
                command.ExecuteNonQuery();
            }
        }

        public List<ProviderEntity> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Provider";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var list = new List<ProviderEntity>();
                while (reader.Read())
                {
                    var provider = new ProviderEntity();
                    provider.Id = Convert.ToInt32(reader["Id"]);
                    provider.Name = Convert.ToString(reader["Name"]);
                    list.Add(provider);
                }

                return list;
            }
        }

        public ProviderEntity Get(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Provider where Id=@Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var provider = new ProviderEntity();
                    return provider;
                }
                else
                {
                    return null;
                }

            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "delete from Provider where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}