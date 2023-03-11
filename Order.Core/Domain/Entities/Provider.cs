using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string SerialNumber { get; set; }
        public string FIN { get; set; }
        public string Authority { get; set; }
        public DateTime GivingDate { get; set; }
        public DateTime ReliabilityDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string RegistrationAddressCountry { get; set; }
        public string RegistrationAddressCity { get; set; }
        public string RegistrationAddressStreet { get; set; }
        public string MailingAddress1 { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ActualAddressCountry { get; set; }
        public string ActualAddressCity { get; set; }
        public string ActualAddressStreet { get; set; }
        public string MailingAddress2 { get; set; }
        public decimal MonthlySalary { get; set; }
        public string Position { get; set; }
        public string GUARDIAN { get; set; }
        public string NATIONID { get; set; }
        public bool IsConviction { get; set; }
        public string ConvictionText { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
    }
}
