using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentOrder.Core.Domain.Abstract;
using PaymentOrder.Core.Domain.Entities;

namespace PaymentOrder.Core.DataAccess.Sql
{
    public class SqlOrderRepository:IOrderRepository
    {
        private readonly string _connect;

        public SqlOrderRepository(string connect)
        {
            _connect = connect;
        }
        public void Add(Order bank)
        {
            using (var connection=new SqlConnection(_connect))
            {
                connection.Open();
                string query = "Insert into Banks output inserted.id values(@Name,@VOEN,@CorrespondentAccount,@SWIFT,@CreatorId,@modifiedDate,@IsDeleted)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("name", bank.Name);
                command.Parameters.AddWithValue("voen", bank.VOEN);
                command.Parameters.AddWithValue("correspondentaccount", bank.CorrespondentAccount);
                command.Parameters.AddWithValue("swift", bank.SWIFT);
                command.Parameters.AddWithValue("creatorid", bank.CreatorId);
                command.Parameters.AddWithValue("modifiedDate", bank.ModifiedDate);
                command.Parameters.AddWithValue("isdeleted", bank.IsDeleted);
                bank.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(Order bank)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query =
                    "update Banks set Name=@name,VOEN=@voen,CorresPondentAccount=@correspondentaccount,SWIFT=@swift,CreatorId=@creatorid,modifiedDate=@modifiedDate,IsDeleted=@isdeleted where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", bank.Id);
                command.Parameters.AddWithValue("name", bank.Name);
                command.Parameters.AddWithValue("voen", bank.VOEN);
                command.Parameters.AddWithValue("correspondentaccount", bank.CorrespondentAccount);
                command.Parameters.AddWithValue("swift", bank.SWIFT);
                command.Parameters.AddWithValue("creatorid", bank.CreatorId);
                command.Parameters.AddWithValue("modifiedDate", bank.ModifiedDate);
                command.Parameters.AddWithValue("isdeleted", bank.IsDeleted);
                command.ExecuteNonQuery();
            }
        }

        public List<Order> GetAll()
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Banks where IsDeleted = 0";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var list = new List<Order>();
                while (reader.Read())
                {
                   var bank = new Order();
                   list.Add(bank);
                }

                return list;
            }
        }

        public Order Get(int id)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Banks where Id= @id and IsDeleted = 0";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var bank = new Order();
                    return bank;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "delete from Banks where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();

            }
        }

    }
}
