using Infrastructure.Models.Attributes;
using System;

namespace Infrastructure.Models.Identity
{
    [Table(Name = "Client")]
    public class Client : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PassportSeria { get; set; }
        public string PassportNo { get; set; }
        public string IssuedBy { get; set; }
        public DateTime IssueDate { get; set; }
        public string IdentificationNo { get; set; }
        public string PlaceOfBirth { get; set; }
        public long ActualResidenceCityId { get; set; }
        public string ActualResidenceAddress { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string WorkPlace { get; set; }
        public string WorkPosition { get; set; }
        public long RegistrationCityId { get; set; }
        public long MaritalStatusId { get; set; }
        public long CitizenshipCountryId { get; set; }
        public long DisabilityId { get; set; }
        public bool IsPensioner { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}
