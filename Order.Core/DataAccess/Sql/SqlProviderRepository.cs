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
        private readonly string _connect;

        public SqlProviderRepository(string connect)
        {
            _connect = connect;
        }

        public void Add(ProviderEntity resident)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query =
                    "Insert into Residents output inserted.id values(@FirstName,@LastName,@FatherName,@DocumentType,@SerialNumber,@FIN,@Authority,@GivingDate,@ReliabilityDate,@MartialStatus,@Gender,@BirthDate,@BirthCountry,@RegistrationAddressCountry,@RegistrationAddressCity,@RegistrationAddressStreet,@MailingAddress1,@Citizenship,@PhoneNumber,@Username,@ActualAddressCountry,@ActualAddressCity,@ActualAddressStreet,@MailingAddress2,@MonthlySalary,@IncomeSource,@Education,@Position,@GUARDIAN,@NATIONID,@IsConviction,@Currency,@Code,@IsDeleted)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("firstname", resident.FirstName);
                command.Parameters.AddWithValue("lastname", resident.LastName);
                command.Parameters.AddWithValue("fathername", resident.FatherName);
                command.Parameters.AddWithValue("serialnumber", resident.SerialNumber);
                command.Parameters.AddWithValue("fin", resident.FIN);
                command.Parameters.AddWithValue("authority", resident.Authority);
                command.Parameters.AddWithValue("givingdate", resident.GivingDate);
                command.Parameters.AddWithValue("reliabilitydate", resident.ReliabilityDate);
                command.Parameters.AddWithValue("birthdate", resident.BirthDate);
                command.Parameters.AddWithValue("birthcountry", resident.BirthCountry);
                command.Parameters.AddWithValue("registrationaddresscountry", resident.RegistrationAddressCountry);
                command.Parameters.AddWithValue("registrationaddresscity", resident.RegistrationAddressCity);
                command.Parameters.AddWithValue("registrationaddressstreet", resident.RegistrationAddressStreet);
                command.Parameters.AddWithValue("mailingaddress1", resident.MailingAddress1);
                command.Parameters.AddWithValue("citizenship", resident.Citizenship);
                command.Parameters.AddWithValue("phonenumber", resident.PhoneNumber);
                command.Parameters.AddWithValue("email", resident.Email);
                command.Parameters.AddWithValue("actualaddresscountry", resident.ActualAddressCountry);
                command.Parameters.AddWithValue("actualaddresscity", resident.ActualAddressCity);
                command.Parameters.AddWithValue("actualaddressstreet", resident.ActualAddressStreet);
                command.Parameters.AddWithValue("mailingaddress2", resident.MailingAddress2);
                command.Parameters.AddWithValue("monthlysalary", resident.MonthlySalary);
                command.Parameters.AddWithValue("position", resident.Position);
                command.Parameters.AddWithValue("guardian", resident.GUARDIAN);
                command.Parameters.AddWithValue("nationid", resident.NATIONID);
                command.Parameters.AddWithValue("isconviction", resident.IsConviction);
                command.Parameters.AddWithValue("code", resident.Code);
                command.Parameters.AddWithValue("isdeleted", resident.IsDeleted);
                resident.Id = Convert.ToInt32(command.ExecuteScalar());

            }
        }

        public void Update(ProviderEntity resident)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query =
                    "update Residents set FirstName=@firstname,LastName=@lastname,FatherName=@fathername,DocumentType=@documenttype,SerialNumber=@serialnumber,FIN=@fin,Authority=@authority,GivingDate=@givingdate,ReliabilityDate=@reliabilitydate,MartialStatus=@martialstatus,Gender=@gender,BirthDate=@birthdate,BirthCountry=@birthcountry,RegistrationAddressCountry=@registrationaddresscountry,RegistrationAddressCity=@registrationaddresscity,RegistrationAddressStreet=@registrationaddressstreet,MailingAddress1=@mailingaddress1,Citizenship=@citizenship,PhoneNumber=@phonenumber,Username=@email,ActualAddressCountry=@actualaddresscountry,ActualAddressCity=@actualaddresscity,ActualAddressStreet=@actualaddressstreet,MailingAddress2=@mailingaddress2,MonthlySalary=@monthlysalary,IncomeSource=@incomesource,Education=@education,Position=@position,GUARDIAN=@guardian,NATIONID=@nationid,IsConviction=@isconviction,Currency=@currency,Code=@code,IsDeleted=@isdeleted where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", resident.Id);
                command.Parameters.AddWithValue("firstname", resident.FirstName);
                command.Parameters.AddWithValue("lastname", resident.LastName);
                command.Parameters.AddWithValue("fathername", resident.FatherName);
                command.Parameters.AddWithValue("serialnumber", resident.SerialNumber);
                command.Parameters.AddWithValue("fin", resident.FIN);
                command.Parameters.AddWithValue("authority", resident.Authority);
                command.Parameters.AddWithValue("givingdate", resident.GivingDate);
                command.Parameters.AddWithValue("reliabilitydate", resident.ReliabilityDate);
                command.Parameters.AddWithValue("birthdate", resident.BirthDate);
                command.Parameters.AddWithValue("birthcountry", resident.BirthCountry);
                command.Parameters.AddWithValue("registrationaddresscountry", resident.RegistrationAddressCountry);
                command.Parameters.AddWithValue("registrationaddresscity", resident.RegistrationAddressCity);
                command.Parameters.AddWithValue("registrationaddressstreet", resident.RegistrationAddressStreet);
                command.Parameters.AddWithValue("mailingaddress1", resident.MailingAddress1);
                command.Parameters.AddWithValue("citizenship", resident.Citizenship);
                command.Parameters.AddWithValue("phonenumber", resident.PhoneNumber);
                command.Parameters.AddWithValue("email", resident.Email);
                command.Parameters.AddWithValue("actualaddresscountry", resident.ActualAddressCountry);
                command.Parameters.AddWithValue("actualaddresscity", resident.ActualAddressCity);
                command.Parameters.AddWithValue("actualaddressstreet", resident.ActualAddressStreet);
                command.Parameters.AddWithValue("mailingaddress2", resident.MailingAddress2);
                command.Parameters.AddWithValue("monthlysalary", resident.MonthlySalary);
                command.Parameters.AddWithValue("position", resident.Position);
                command.Parameters.AddWithValue("guardian", resident.GUARDIAN);
                command.Parameters.AddWithValue("nationid", resident.NATIONID);
                command.Parameters.AddWithValue("isconviction", resident.IsConviction);
                command.Parameters.AddWithValue("code", resident.Code);
                command.Parameters.AddWithValue("isdeleted", resident.IsDeleted);
                command.ExecuteNonQuery();
            }
        }

        public List<ProviderEntity> GetAll()
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Residents where IsDeleted=0";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var list = new List<ProviderEntity>();
                while (reader.Read())
                {
                    var resident = new ProviderEntity();
                    list.Add(resident);
                }

                return list;
            }
        }

        public ProviderEntity Get(int id)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Residents where Id=@id and IsDeleted=0";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var resident = new ProviderEntity();
                    return resident;
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
                string query = "delete from Residents where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}